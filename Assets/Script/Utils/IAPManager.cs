using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;


namespace Script.Utils {
    public class IAPManager: MonoBehaviour, IStoreListener {
        
        private static IStoreController storeControler;
        public ShopApp shopApp;

        
        private string[] sProductIds = {
            "item_passive_number5", 
            "item_passive_airpods", 
            "item_passive_tumbler",
            "item_passive_redginseng",
        };
        

        private void Start() {
            if (storeControler == null) {
                Debug.Log("storeControler == 널");
                initStore();
            }
            else Debug.Log("storeControler!=널");
        }

        
        
        void initStore() {
            
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            for (int i = 0; i < sProductIds.Length; i++) {
                builder.AddProduct(sProductIds[i], ProductType.NonConsumable, new IDs{ {sProductIds[i], GooglePlay.Name} });
            }
            UnityPurchasing.Initialize(this, builder);
        }

        public void purchaseItem(int item) {

            if (storeControler == null) {
                initStore();
                Debug.Log("결재실패. 원인:결재기능 초기화 실패 -> 재시도");
            }
            else {

                int index;
                
                if (item == 400) index = 0; //샤넬No5
                else if (item == 401) index = 1; //에어팟
                else if (item == 402) index = 2; //스벅텀블러
                else if (item == 403) index = 3; //정관장홍삼정
                else return;
                
                storeControler.InitiatePurchase(sProductIds[index]);                
            }

        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e) {
            bool isSuccess = true;
            
            
            #if UNITY_ANDROID && !UNITY_EDITOR
                CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), 
                AppleTangle.Data(), Application.identifier);
    
                try {
                    IPurchaseReceipt[] result = validator.Validate(e.purchasedProduct.receipt);
                    for (int i = 0; i < result.Length; i++) {
                        Analytics.Transaction(result[i].productID, e.purchasedProduct.metadata.localizedPrice,
                            e.purchasedProduct.metadata.isoCurrencyCode, result[i].transactionID, null);
                    }
                }
                catch (IAPSecurityException) {
                    isSuccess = false;
                }
            #endif
            
            if(isSuccess==false) Debug.Log("인앱결제 isSuccess==false");
                     

            if (isSuccess == true) {
                Debug.Log("구매완료");
                
                if(e.purchasedProduct.definition.id.Equals(sProductIds[0])){ //샤넬 넘버5
                    buyNumber5();
                }
                else if(e.purchasedProduct.definition.id.Equals(sProductIds[1])){ //에어팟
                    buyAirPods();                    
                }
                else if (e.purchasedProduct.definition.id.Equals(sProductIds[2])) { //스벅텀블러
                    buyTumbler();
                }
                else if (e.purchasedProduct.definition.id.Equals(sProductIds[3])) { //정관장 홍삼정
                    buyRedGinseng();
                }
            }

            return PurchaseProcessingResult.Complete;
        }




        private void buyNumber5() {
            Debug.Log("샤넬No5 삼");
            global::Utils.addMyItem(400);     
            shopApp.refreshState();
        }
        
        private void buyAirPods() {
            Debug.Log("에어팟 삼");
            global::Utils.addMyItem(401);
            shopApp.refreshState();
        }

        private void buyTumbler() {
            Debug.Log("텀블러 삼");
            global::Utils.addMyItem(402);
            shopApp.refreshState();
        }

        private void buyRedGinseng() {
            Debug.Log("홍삼정 삼");
            global::Utils.addMyItem(403);
            shopApp.refreshState();
        }

        
        
        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
            storeControler = controller;
            Debug.Log("인앱결재 초기화 성공");            
        }
        
        public void OnInitializeFailed(InitializationFailureReason error) {
            Debug.Log("OnInitializeFailed: " + error);
        }
        
        public void OnPurchaseFailed(Product i, PurchaseFailureReason p) {
            Debug.Log("OnPurChaseFailed: " + i + ", " + p);
        }
    }
}