Feature: Products Filtering
  In order verify the products filtering
  As a consumer of the automation stores website
  I want to ensure that the right number of products and its prices are displayed when a particular category of product is filtered

@productFiltering
Scenario: Filter products of a particular category
	Given the user navigates to automation stores home page
	When the user selects <subCategory> from <mainCategory> or <SaleItem>
	Then the correct number of products under <subCategory> is listed
Examples:
	| mainCategory          | subCategory | SaleItem |
	| Apparel & accessories | Shoes       | No       |
	| Apparel & accessories | T-shirts    | No       |

@productFiltering
Scenario: Product price is displayed correctly for different currencies
	Given the user navigates to automation stores home page
	When the user selects <subCategory> from <mainCategory> or <SaleItem>
	When the user selects <currency> option
	Then the correct number of products under <subCategory> is listed and price is displayed in selected <currency>
Examples:
	| mainCategory          | subCategory | currency         | SaleItem |
	| Apparel & accessories | Shoes       | $ US Dollar      | No       |
	| Apparel & accessories | Shoes       | £ Pound Sterling | No       |
	| Apparel & accessories | Shoes       | € Euro           | No       |
	| Apparel & accessories | T-shirts    | $ US Dollar      | No       |
	| Apparel & accessories | T-shirts    | £ Pound Sterling | No       |
	| Apparel & accessories | T-shirts    | € Euro           | No       |



