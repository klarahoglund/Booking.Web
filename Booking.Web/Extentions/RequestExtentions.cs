namespace Booking.Web.Extentions
{
    public static class RequestExtentions //Används av allt
    {

        public static bool IsAjax(this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
