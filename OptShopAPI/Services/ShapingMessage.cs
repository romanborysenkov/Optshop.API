using Microsoft.EntityFrameworkCore;
using OptShopAPI.Data;
using OptShopAPI.Models;

namespace OptShopAPI.Services
{
    public static class ShapingMessage
    {

       static DataContext _context;

        public static string InfoAboutCustomerAndOrdered(Customer customer, List<Product> customedProducts, List<Order> orderedProductsIdsAndCount)
        {
            string message = "<b>Інформація про замовника:</b> \n";
            message += "Ім'я людини: " + customer.username +
                "\nEmail: " + customer.email + "\nНомер телефону: +" + customer.phoneNumber +
                "\nКраїна: " + customer.country +
                "\nПровінція/Область: " + customer.province + "\nМісто: " + customer.city +
                "\nВулиця: " + customer.streetAddress + "\nНомер будинку: " + customer.houseNumber
                + "\nПоштовий індекс: " + customer.index;
               
               
            message += "\n\n Замовлення: \n\n";
            for(int i = 0; i < orderedProductsIdsAndCount.Count; i++)
            {
                message +="<b>Замовлення №: "+(i+1)+ "</b>\nНазва: " + customedProducts[i].name +"\nОригінальне посилання:"+customedProducts[i].OriginalLink + "\nЦіна: " + customedProducts[i].price +
                    "$\nКолір: " + orderedProductsIdsAndCount[i].color + "\nКількість: "+ 
                    orderedProductsIdsAndCount[i].productCount + "\n"+"Коментар: "+ orderedProductsIdsAndCount[i].description+"\n\n";
            }
          
            return message;
        }

        public static string InfoAboutOrdered(List<Product> customedProducts, List<Order> orderedProductsIdsAndCount)
        {
            string message = "\n\n Замовлення оплачено повністю: \n\n";
            for (int i = 0; i < orderedProductsIdsAndCount.Count; i++)
            {
                message += "<b>Замовлення №: " + (i + 1) + "</b>\nНазва: " + customedProducts[i].name + "\nОригінальне посилання:" + customedProducts[i].OriginalLink + "\nЦіна: " + customedProducts[i].price +
                    "$\nКолір: " + orderedProductsIdsAndCount[i].color + "\nКількість: " +
                    orderedProductsIdsAndCount[i].productCount + "\n" + "Коментар: " + orderedProductsIdsAndCount[i].description + "\n\n";
            }


            return message;
        }

        public static string OrderList(string orderids, DataContext context)
        {
            _context = context;   
            List<string> ordersId = new List<string>();

            ordersId = StringService.SplitString(orderids);

            List<Product> orderedProducts = new List<Product>();

            List<Order> orderedProductsIdsAndCount = new List<Order>();

            foreach (var item in ordersId)
            {
                orderedProductsIdsAndCount.Add(_context.orders.First(x => x.Id == Guid.Parse(item)));
            }

            foreach (var item in orderedProductsIdsAndCount)
            {
                orderedProducts.Add(_context.products.First(x => x.id.ToString() == item.productId));
            }

            string message = ShapingMessage.InfoAboutOrdered( orderedProducts, orderedProductsIdsAndCount);
            return message;
        }
    }
}
