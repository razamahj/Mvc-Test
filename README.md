# MVC Test

## Scenario
We have a single point of contact that receives referrals for a number of services.  Staff working in the single point of contact need to be able to record these referrals onto the system which will then notify the manager of the service so they are aware of the new referral.

## Restrictions
The system is an MVC web app using Entity Framework Core to store the data in a Microsoft SQL Server Database.  There is an existing Entity Framework database context that represents the database structure and contains the fields that should be recorded when adding a referral and this should be used for all data access.

## Requirements
Users need to be able to add new referrals onto the system with the following rules:

The following fields of data must be recorded:

- Forename
- Surname
- Date of Birth
- Date of Referral
- The service the referral relates to

At least one of the following fields are required:
- Clients Contact Telephone Number
- Clients Email Address


Clients must be at least 18 years old on the date of the referral and if a client already exists on the system when a new referral is added then the referral should be linked to the existing client record so that duplicate client records are not created in the database.

When a new referral is recorded then the manager of the service that the referral is for needs to be notified of the new referral via email.

Additionally users need to be able to see a list of referrals on the system with the most recent ones first.  All the information recorded for each referral should be shown in the list and the user should be able to filter the list using the clients name so they can easily see if a referral has already been recorded on the system.

Please clone this repository and then complete your changes in your own repository and then supply a link to a public repository containing your work.
