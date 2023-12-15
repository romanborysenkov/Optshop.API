namespace OptShopAPI.Services
{
    public static class StringService
    {
       public static List<string> SplitString(string input)
        {
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> result = new List<string>(parts);
            return result;
        }

      /*  public static List<string> SplitStringByDash(string input)
        {
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> result = new List<string>(parts);
            return result;
        }*/
    }
}
