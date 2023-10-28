Feature: Adding Products to Cart
  In order verify adding products to cart
  As a consumer of the automation stores website
  I want to ensure that different products can be added to cart

@addProductToCart
Scenario: Add products to cart
	Given the user navigates to automation stores home page
	When the user selects <subCategory> from <mainCategory> or <SaleItem>
	When the user adds the <ProdNo> with <productNameToAdd> with <QtyToAdd> to cart
	Then the <productNameToAdd> should be added to the cart with <QtyToAdd> and <ExpectedTotalPrice> and <ExpectedUnitPrice>
Examples:
	| ProdNo | mainCategory          | subCategory | productNameToAdd                                                              | QtyToAdd | ExpectedTotalPrice | ExpectedUnitPrice | SaleItem |
	| 1      | Apparel & accessories | Shoes       | Womens high heel point toe stiletto sandals ankle strap court shoes           | 30       | $780.00            | $26.00            | No       |
	| 2      | Apparel & accessories | T-shirts    | Designer Men Casual Formal Double Cuffs Grandad Band Collar Shirt Elegant Tie | 10       | $320.00            | $32.00            | No       |
	| 3      |                       |             | LE ROUGE ABSOLU Reshaping & Replenishing LipColour SPF 15                     | 10       | $270.00            | $27.00            | Yes      |


@deleteProductFromCart
Scenario: Add Items to cart and delete an item
	Given the user navigates to automation stores home page
	When the user selects <subCategory> from <mainCategory> or <SaleItem>
	When the user adds the <ProdNo> with <productNameToAdd> with <QtyToAdd> to cart
	When the user deletes the <productNameToAdd> from cart
	Then the <productNameToAdd> should be deleted
Examples:
	| ProdNo | mainCategory          | subCategory | productNameToAdd          | QtyToAdd | ExpectedTotalPrice | ExpectedUnitPrice | SaleItem |
	| 1      | Apparel & accessories | Shoes       | Fiorella Purple Peep Toes | 1        | $110               | $110              | No       |




