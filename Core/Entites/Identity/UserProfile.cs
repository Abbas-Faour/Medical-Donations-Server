namespace Core.Entites.Identity
{
    public class UserProfile
    {
        public int NumberOfRequests { get; set; }
        public int NumberOfOffers { get; set; }
        public int TotalPosts 
        { 
            get{
                return NumberOfRequests + NumberOfOffers;
                }
        }
    }
}