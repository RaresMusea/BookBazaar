# Book Bazaar
An e-commerce, book-selling web application built using ASP .NET Core MVC, .NET 7.0 and SQL Server.
As ORM, the app was developed using Microsoft Entity Framework Core (EF Core).
For authentication and authorization, the app uses the Identity Frameowrk, provided by Microsoft.
The key-features of this ap may be:
- A dyncamic discount system which dyncamically applies discounts to the products based on the quantity.
- Stripe integration for credit card payments and checkout.
- Admin CMS.
- Roles for both customers which are registered as normal users and companies, which have a delayed payment for each order.
- Real time order status check.
- A realistic order status lifecycle, which includes tracking, delivery details and the possibility of reverting a payment or refund.
- A classic recomandation system when looking for products, which is based on their category.
- A low-privilege admin role, called Internal, which is only allowed to modify the stats for the placed orders.

For a better scalability, patterns such as Unit of Work, or Repository were used in order to facilitate a better communication with the persistence layer.

