using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Notifications;
using qlbh.Models;
using System.Net;
using System.Net.Mail;

namespace qlbh.Controllers
{
    public class GiohangController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();
        // GET: Giohang
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }
        //khai báo phương thức thêm sản phẩm vào giỏ hàng
        public RedirectToRouteResult AddToCard(Guid MaSP)
        {
            if(Session["giohang"]==null)
            {
                Session["giohang"] = new List<CartItem>();
            }
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            //kiểm tra sản phẩm khách đang chọn có trong giỏ hàng
            if(giohang.FirstOrDefault(m=>m.MaSP.ToString()==MaSP.ToString())==null)//chưa có trong giỏ hàng
            {
                SanPham sp = db.SanPhams.Find(MaSP);
                CartItem newItem = new CartItem();
                newItem.MaSP = MaSP.ToString();
                newItem.TenSP = sp.TenSP;
                newItem.SoLuong = 1;
                newItem.DonGia = Convert.ToDecimal(sp.Dongia);
                giohang.Add(newItem);
            }
            else//sản phẩm đã có trong giỏ hàng=> tăng số lượng lên 1
            {
                CartItem cartItem = giohang.FirstOrDefault(m => m.MaSP.ToString() == MaSP.ToString());
                cartItem.SoLuong++;
            }
            Session["giohang"] = giohang;
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Update(Guid MaSP, int txtSoLuong)
        {
            //Tim CartItem muon xoa
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem item = giohang.FirstOrDefault(m => m.MaSP.ToString()== MaSP.ToString());
            if(item!=null)
            {
                item.SoLuong = txtSoLuong;
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult DelCartItem(Guid MaSP)
        {
            //Tim CartItem muon xoa
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem item = giohang.FirstOrDefault(m => m.MaSP.ToString() == MaSP.ToString());
            if (item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Oder(string email, string phone)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            string sMsg = "<html><body><table boder='1'><caption>Thông tin đặt hàng</caption><tr><th>STT</th><th>Tên hàng</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr>";
            int i = 0;
            decimal tongtien = 0;
            foreach(CartItem item in giohang)
            {
                i++;
                sMsg += "<tr>";
                sMsg += "<td>"+i.ToString()+"</td>";
                sMsg += "<td>"+item.TenSP+"</td>";
                sMsg += "<td>"+item.SoLuong.ToString()+"</td>";
                sMsg += "<td>"+String.Format("{0:#,###}",item.SoLuong*item.DonGia)+"</td>";
                sMsg += "<tr>";
                tongtien += item.SoLuong * item.DonGia;
            }
            sMsg += "<tr><th colspan='5'>Tổng cộng: "
                + String.Format("{0:#,### vnđ}", tongtien) + "</th></tr><table>";
            MailMessage mail = new MailMessage("ngocthienTCVan.b10@gmail.com", email, "Thông tin đơn hàng", sMsg);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("emailcuaban", "matkhau");
            mail.IsBodyHtml = true;
            client.Send(mail);
            return RedirectToAction("Index", "HoaDon");
            //gửi mail cho khách hàng
        }

    }
}