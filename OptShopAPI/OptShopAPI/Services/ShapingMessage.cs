using OptShopAPI.Models;

namespace OptShopAPI.Services
{
    public static class ShapingMessage
    {

        public static string InfoAboutCustomerAndOrdered(Customer customer, List<Product> customedProducts, List<Order> orderedProductsIdsAndCount)
        {
            string message = "<b>Інформація про замовника:</b> \n";
            message += "Ім'я людини: " + customer.username +
                "\nEmail: " + customer.email + "\nНомер телефону: +" + customer.phoneNumber +
                "\nКраїна: " + customer.country + 
                "\nПровінція/Область: " + customer.province + "\nМісто: " + customer.city +
                "\nВулиця: " + customer.streetAddress + "\nНомер будинку: " + customer.houseNumber
                + "\nПоштовий індекс: " + customer.postalCode  + "\nPLZ: " + customer.plz +
                "\nMailbox: " + customer.mailbox + "\nEircode: " + customer.eircode + "\nZIP code: " +
                customer.zip_code;
            message += "\n\n Замовлення: \n\n";
            for(int i = 0; i < orderedProductsIdsAndCount.Count; i++)
            {
                message +="<b>Замовлення №: "+(i+1)+ "</b>\nНазва: " + customedProducts[i].name + "\nЦіна: " + customedProducts[i].price +
                    "$\nКолір: " + orderedProductsIdsAndCount[i].color + "\nКількість: "+ 
                    orderedProductsIdsAndCount[i].productCount + "\n"+"Коментар: "+ orderedProductsIdsAndCount[i].description+"\n\n";
            }
           

            return message;
        }
    }
}
