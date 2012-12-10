using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


namespace CheckOut_Kata.Tests
{
    [TestFixture]
    public class ICheckOutTests
    {
        ICheckout checkout;

        [SetUp]
        public void Setup()
        {
            checkout = new Checkout(new Calculator(new PriceCalculator(), new DiscountCalculator(new Discounts())), new ShoppingCart(new Products()) );
        }

        [Test]
        public void ICheckout_Scan_TakesString()
        {

            checkout.Scan("");
            Assert.True(true);
        }

        [Test]
        public void ICheckout_Total_ReturnsInt()
        {
            var total = checkout.Total();
            Assert.IsInstanceOf<int>(total);
        }
    }

    [TestFixture]
    public class CheckoutTests
    {
        private ICheckout checkout;

        [SetUp]
        public void Setup()
        {
            checkout = new Checkout(new Calculator(new PriceCalculator(), new DiscountCalculator(new Discounts())), new ShoppingCart(new Products()));
        }
        [Test]
        public void ScanEmptyValuesZero()
        {
            // Arrange
            const string emptyProduct = "";

            // Act
            checkout.Scan(emptyProduct);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(0));
        }

        [Test]
        public void ScanOneAValues50()
        {
            // Arrange
            const string productA = "A";

            // Act
            checkout.Scan(productA);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(50));
        }

        [Test]
        public void ScanTwoAsValues100()
        {
            // Arrange
            const string productA = "A";

            // Act
            checkout.Scan(productA);
            checkout.Scan(productA);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(100));
        }

        [Test]
        public void ScanOneBValues30()
        {
            // Arrange
            const string productB = "B";

            // Act
            checkout.Scan(productB);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(30));
        }

        [Test]
        public void ScanTwoBsValues45()
        {
            // Arrange
            const string productB = "B";

            // Act
            checkout.Scan(productB);
            checkout.Scan(productB);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(45));
        }

        [Test]
        public void Scan_A_and_B_is_same_as_B_and_A()
        {
            // Arrange
            var checkoutAB = new Checkout(new Calculator(new PriceCalculator(), new DiscountCalculator(new Discounts())), new ShoppingCart(new Products()));
            var checkoutBA = new Checkout(new Calculator(new PriceCalculator(), new DiscountCalculator(new Discounts())), new ShoppingCart(new Products()));

            const string productA = "A";
            const string productB = "B";

            // Act
            checkoutAB.Scan(productA);
            checkoutAB.Scan(productB);

            checkoutBA.Scan(productB);
            checkoutBA.Scan(productA);

            var totalAB = checkoutAB.Total();
            var totalBA = checkoutBA.Total();

            // Assert
            Assert.AreEqual(totalAB, totalBA);
        }

        [Test]
        public void ScanOneAAndOneBValues80()
        {
            // Arrange
            const string productA = "A";
            const string productB = "B";

            // Act
            checkout.Scan(productA);
            checkout.Scan(productB);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(80));
        }

        [Test]
        public void ScanThreeAsValues130()
        {
            // Arrange
            const string productA = "A";

            // Act
            checkout.Scan(productA);
            checkout.Scan(productA);
            checkout.Scan(productA);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(130));
        }

        [Test]
        public void ScanThreeAsAn2BsValues175()
        {
            // Arrange
            const string productA = "A";
            const string productB = "B";

            // Act
            checkout.Scan(productA);
            checkout.Scan(productA);
            checkout.Scan(productA);
            checkout.Scan(productB);
            checkout.Scan(productB);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(175));
        }

        [Test]
        public void Scan2BsValues45()
        {
            // Arrange
            const string productB = "B";

            // Act
            checkout.Scan(productB);
            checkout.Scan(productB);
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(45));
        }

    }

    [TestFixture]
    public class DiscountCalculatorTest
    {
        private IDiscounts discounts = new Discounts();

        [Test]
        public void TotalDiscountForShoppingCartWith3AsIs20()
        {
            var shoppingCart = new Dictionary<Product, int>();
            var productA = new Products().GetByCode("A");
            shoppingCart.Add(productA, 3);
            var discount = new DiscountCalculator(discounts).GetDiscount(shoppingCart);
            Assert.AreEqual(20, discount);
        }

        [Test]
        public void TotalDiscountForShoppingCartWith6AsIs40()
        {
            var shoppingCart = new Dictionary<Product, int>();
            var productA = new Products().GetByCode("A");
            shoppingCart.Add(productA, 6);
            var discount = new DiscountCalculator(discounts).GetDiscount(shoppingCart);
            Assert.AreEqual(40, discount);
        }

        [Test]
        public void TotalDiscountForShoppingCartWith3AsAnd2BsIs35()
        {
            var shoppingCart = new Dictionary<Product, int>();
            var productA = new Products().GetByCode("A");
            var productB = new Products().GetByCode("B");
            shoppingCart.Add(productA, 3);
            shoppingCart.Add(productB, 2);
            var discount = new DiscountCalculator(discounts).GetDiscount(shoppingCart);
            Assert.AreEqual(35, discount);
        }

        [Test]
        public void TotalDiscountForOneAIs0()
        {
            var discount = new DiscountCalculator(discounts).DiscountFor("A", 1);

            Assert.AreEqual(discount, 0);
        }

        [Test]
        public void TotalDiscountForTwoAsIs0()
        {
            var discount = new DiscountCalculator(discounts).DiscountFor("A", 2);

            Assert.AreEqual(discount, 0);
        }

        [Test]
        public void TotalDiscountForThreeAsIs20()
        {
            var discount = new DiscountCalculator(discounts).DiscountFor("A", 3);

            Assert.AreEqual(discount, 20);
        }

        [Test]
        public void TotalDiscountForOneBsIs0()
        {
            var discount = new DiscountCalculator(discounts).DiscountFor("A", 3);

            Assert.AreEqual(discount, 20);
        }

        [Test]
        public void TotalDiscountForTwoBsIs15()
        {
            var discount = new DiscountCalculator(discounts).DiscountFor("A", 3);

            Assert.AreEqual(discount, 20);
        }

        [Test]
        public void TotalDiscountForThreeBsIs15()
        {
            var discount = new DiscountCalculator(discounts).DiscountFor("A", 3);

            Assert.AreEqual(discount, 20);
        }

        [Test]
        public void TotalDiscountForFourBsIs60()
        {
            var discount = new DiscountCalculator(discounts).DiscountFor("A", 3);

            Assert.AreEqual(discount, 20);
        }
    }
}

namespace CheckOut_Kata
{
    class CheckOutKata
    {
        static void Main(string[] args)
        {
        }
    }

    public class ShoppingCart : Dictionary<Product,int>
    {
        private readonly IProducts products;

        public ShoppingCart(IProducts _products)
        {
            products = _products;
        }

        public void Add(string productCode)
        {
            var product = products.GetByCode(productCode);

            if (!ContainsKey(product))
            {
                Add(product, 1);
            }
            else
            {
                this[product]++;
            }
        }

    }

    public interface IShoppingCart
    {
        void Add(string productCode);
    }

    public class Checkout : ICheckout
    {
        private readonly ICalculator calculator;

        private readonly ShoppingCart shoppingCart;

        public Checkout(ICalculator _calculator,  ShoppingCart _shoppingCart)
        {
            calculator = _calculator;
            shoppingCart = _shoppingCart;
        }

        public void Scan(string productCode)
        {
            shoppingCart.Add(productCode);
        }

        public int Total()
        {
            var price = calculator.GetPrice(shoppingCart);

            var discount = calculator.GetDiscount(shoppingCart);

            return (price - discount);
        }


    }

    public interface ICalculator
    {
        int GetPrice(Dictionary<Product, int> shoppingCart);
        int GetDiscount(Dictionary<Product, int> shoppingCart);
    }

    public class Calculator : ICalculator
    {
        private readonly PriceCalculator priceCalculator;
        private readonly DiscountCalculator discountCalculator;

        public Calculator(PriceCalculator _priceCalculator, DiscountCalculator _discountCalculator)
        {
            priceCalculator = _priceCalculator;
            discountCalculator = _discountCalculator;
        }

        public int GetPrice(Dictionary<Product, int> shoppingCart)
        {
            return priceCalculator.GetPrice(shoppingCart);
        }

        public int GetDiscount(Dictionary<Product, int> shoppingCart)
        {
            return discountCalculator.GetDiscount(shoppingCart);
        }
    }

    public class PriceCalculator
    {

        public int GetPrice(Dictionary<Product, int> shoppingCart)
        {
            var priceWithoutDiscount = 0;

            foreach (var product in shoppingCart)
            {
                var productQuantity = product.Value;
                var productPrice = product.Key.Cost;

                priceWithoutDiscount += productPrice * productQuantity;
            }
            return priceWithoutDiscount;
        }
    }

    public class DiscountCalculator
    {

        private IDiscounts discounts;
        public DiscountCalculator(IDiscounts _discounts)
        {
            discounts = _discounts;
        }
        public int DiscountFor(string productCode, int quantity)
        {
            var discount = discounts.GetByCode(productCode);

            return (quantity/discount.PromoQty)*discount.PromoPrice;
        }


        public int GetDiscount(Dictionary<Product, int> products)
        {
            var discount = 0;
            foreach (var product in products)
            {
                discount += DiscountFor(product.Key.Code, product.Value);

            }
            return discount;
        }
    }

    public class ProductOffer
    {
        public ProductOffer(int promoQty,int promoPrice)
        {
            PromoPrice = promoPrice;
            PromoQty = promoQty;
        }
        public int PromoQty { get; private set; }
        public int PromoPrice { get; private set; }
    }

    public class Product
    {
        protected bool Equals(Product other)
        {
            return Cost == other.Cost && string.Equals(Code, other.Code);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Cost * 397) ^ (Code != null ? Code.GetHashCode() : 0);
            }
        }

        public int Cost { get; private set; }

        public string Code { get; private set; }

        public Product(string _code, int _cost)
        {
            Cost = _cost;
            Code = _code;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product)obj);
        }
    }

    public interface IDiscounts
    {
        ProductOffer GetByCode(string productCode);
    }

    public interface IProducts
    {
        Product GetByCode(string productCode);
    }

    public class Products : IProducts
    {
        public Dictionary<string, int> products = new Dictionary<string, int>();
        private readonly Product NullProduct = new Product("", 0);

        public Products()
        {
            products.Add("A", 50);
            products.Add("B", 30);
        }


        public Product GetByCode(string productCode)
        {
            if (products.ContainsKey(productCode))
            {
                return new Product(productCode, products[productCode]);
            }
            return NullProduct;
        }
    }

    public class Discounts : IDiscounts
    {
        protected Dictionary<string, ProductOffer> ProductDiscountTable = new Dictionary<string, ProductOffer>();

        private readonly ProductOffer NoDiscount = new ProductOffer(1, 0);

        public Discounts()
        {
            ProductDiscountTable.Add("A", new ProductOffer(3, 20));
            ProductDiscountTable.Add("B", new ProductOffer(2, 15));
            ProductDiscountTable.Add("C", new ProductOffer(1, 0));
        }

        public ProductOffer GetByCode(string productCode)
        {
            if (ProductDiscountTable.ContainsKey(productCode))
            {
                return ProductDiscountTable[productCode];
            }
            return NoDiscount;
        }
    }

    public interface ICheckout
    {
        void Scan(string _productCode);
        int Total();
    }
}
