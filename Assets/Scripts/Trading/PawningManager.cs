using Customer;
using Item;
using User;
using Util;


namespace Trading
{
    public class PawningManager : Singleton<PawningManager>
    {
        private CustomerBehaviour _currentCustomer; // the customer that is being served
        /// <summary>
        /// The customer offers the player an item for a price
        /// </summary>
        /// <param name="item">The item for sale</param>
        /// <param name="offeringAmount">The initial amount the customer wants for it</param>
        /// <param name="customer">The new customer to serve</param>
        public void OfferUserItem(Items item,int offeringAmount,CustomerBehaviour customer)
        {
            _currentCustomer = customer;
        }
        /// <summary>
        /// The customer tries to buy an item from the player
        /// </summary>
        /// <param name="customer">The new customer to serve</param>
        /// <returns>The Item to buy</returns>
        public Items RequestUserItem(CustomerBehaviour customer)
        {
            _currentCustomer = customer;
            return UserData.Instance.randomItem;
        }
    }
}
