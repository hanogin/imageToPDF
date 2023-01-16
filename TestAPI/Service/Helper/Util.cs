namespace Service.Helper
{
    public static class Util
    {
        public static string GetLetterOfDay(int dayOfTheWeek)
        {
            var day = "";
            switch (dayOfTheWeek)
            {
                case 0:
                    day = "א";
                    break;
                case 1:
                    day = "ב";
                    break;
                case 2:
                    day = "ג";
                    break;
                case 3:
                    day = "ד";
                    break;
                case 4:
                    day = "ה";
                    break;
                case 5:
                    day = "ו";
                    break;
                case 6:
                    day = "מוצש";
                    break;

            }
            return day;
        }
    }
}
