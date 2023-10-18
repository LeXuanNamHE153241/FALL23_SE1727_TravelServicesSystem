using System.Collections.Specialized;
using System.Net;
using System.Text;
using TravelSystem_SWP391.Models;
using TravelSystem_SWP391.Models.PayMents;

namespace TravelSystem_SWP391.DAO_Context
{
    public class PayDAO
    {
        public string payment()
        {

            string vnpUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; // Replace with the actual VNPay API endpoint
            string vnpTmnCode = " A59SHYFX"; // Replace with your merchant code
            string vnpHashSecret = "IHTADPUIOYICGAWMYRBHUWQNBSRMKVZE"; // Replace with your secret key
            string returnUrl = "https://localhost:7153/Booking/ViewListBookingVehicleInTourist"; // Replace with your return URL
            string transactionInfo = "Payment for your order"; // Replace with your order information
            string amount = "100000"; // Replace with the payment amount
           
            // Generate a unique order ID
            string vnpOrderInfo = "1"; // Replace with your order ID

            // Create a dictionary for VNPay parameters
            var vnpParams = new NameValueCollection
        {
            { "vnp_TmnCode", vnpTmnCode },
            { "vnp_Amount", amount },
            { "vnp_Command", "pay" },
            { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
            { "vnp_CurrCode", "VND" },
            { "vnp_Locale", "vn" },
            { "vnp_OrderInfo", transactionInfo },
            { "vnp_OrderType", "topup" },
            { "vnp_ReturnUrl", returnUrl },
            { "vnp_TxnRef", vnpOrderInfo }
        };

            // Calculate the signature
            string vnpSecureHash = GenerateVNPSecureHash(vnpParams, vnpHashSecret);
            vnpParams.Add("vnp_SecureHashType", "MD5");
            vnpParams.Add("vnp_SecureHash", vnpSecureHash);

            // Build the VNPay URL
            string url = vnpUrl + "?" + ToQueryString(vnpParams);

            // Redirect the user to VNPay
            Console.WriteLine("Redirecting to VNPay...");
            Console.WriteLine(url);
            return url;


        }
        public string payment1(int TypePaymentVN)
        {
            var urlPayment = "";
           
            //Get Config Info
            string vnp_Returnurl = "https://localhost:7153/Booking/ViewListBookingVehicleInTourist"; //URL nhan ket qua tra ve 
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = " A59SHYFX"; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = "IHTADPUIOYICGAWMYRBHUWQNBSRMKVZE"; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (long)100000 * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng ");
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", "ROD"); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
            static string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys from value in nvc.GetValues(key) select string.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value))).ToArray();
            return string.Join("&", array);
        }
        public string payment2()
        {
            string vnp_Returnurl = "https://localhost:7153/Booking/ViewListBookingVehicleInTourist"; //URL nhan ket qua tra ve 
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = " A59SHYFX"; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = "IHTADPUIOYICGAWMYRBHUWQNBSRMKVZE"; //Secret Key

            //Get payment input
            OrderInfo order = new OrderInfo();
            order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = 100000; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
            order.CreatedDate = DateTime.Now;
            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            
            vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            
           
              

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "");


            
            vnpay.AddRequestData("vnp_Locale", "vn");
            
           
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
         
            return paymentUrl;
        }

            static string GenerateVNPSecureHash(NameValueCollection nvc, string secretKey)
        {
            string data = ToQueryString(nvc) + secretKey;
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

    }
}
