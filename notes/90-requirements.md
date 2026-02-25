# Software Center 

They maintain the list of supported software for our company.

We are building them an API.

## Vendors

We have arrangements with vendors. Each vendor has:
Vendors are forever. once is created they cannot change name, or id or website.



- And ID we assign
- A Name
- A Website URL
- A Point of Contact
  - Name
  - Email
  - Phone Number


- We have to send them the data above, but without the id.
- We will have some rules, and this is class, so we can em up.

Vendors have a set of software they provide that we support.

PUT /vendors/{id}/pointofcontact

{
  "name": "Sally',
  "email": "Sally@company.com",
  "phone": "999-9999"

}

When we are designing APIs - we really have only three paintbrushes:

- Resource 
  - "An important thingy" - it is a HUMAN thing - computers do not care.
  - We organize our API around the idea of resourcers and hierarchies of resources.
  - https://???/vendors
  - Resources are identifed through URIs (Uniform Resource Identifier)
    - In OOP, duplications is "bad" - Multiple resources that are "aliases" to the same "thing"
    - /employees
    - Subordinate resources that are "documents"
      - /employees/{id}
      - 
- Representation
  - Data we send in the "Body" of some requests (not all requests support representations)
  - or data we get back from the API after an operation.
- Methods (GET POST PUT, DELETE, ...)

POST - consider this entity for membership in your organization.



```http
GET /employees/bob-smith

200 Ok
Content-Type: application/json

{

  "id": "bob-smith",
  "name": {
    "firstName": "Robert",
    "lastName": "Smith"
  },
  "workPhone": "555-1212",
  "job": "Singer/Guitarist for the Cure"

}

GET /employes/bob-smith/manager

{


}

GET /employees/bob-smith/performance-reviews

200 Ok

[]


GET /employees/brad-pitt



## Catalog Items

Catalog items are instances of software a vendor provides.

A catalog item has:
- An ID we assign
- A vendor the item is associated with
- The name of the software item




1. Resources (url) /vendors/{id}/catalog-items
1.1 - Headers

2. Representations

{

  "name": "VsCode"
}

3. Methods - POST 

GET - get it.
POST - collection means add this entity (representation ) to your collection, on a document (rare) - submit this entity for processing.
PUT - REPLACE
DELETE - collection means if you return a success status code, that entire collection should disappear from the WWW

GET /employees

PUT /employees

[

]

GET /employees --> 404

# "Store" - server data that is a "mirror" of client data.

POST /customers/jeff-gonzalez/shopping-cart


POST /shopping-cart 
Authorization: bearer Jeff Gonzalez Token

{
  "qty": 3,
  "size": "large",
  "sku": "Cheap Beer"

}

DELETE /shopping-cart 










## Use Cases

The Software Center needs a way for managers to add vendors. Normal members of the team cannot add vendors.
Software Center team members may add catalog items to a vendor.
Software Center team members may add versions of catalog items.
Software Center may deprecate a catalog items. (effectively retiring them, so they don't show up on the catalog)

Any employee in the company can use our API to get a full list of the software catalog we currently support.

- none of this stuff can be used unless you are verified (identified) as an employee.
- some employees are:
  - members of the software center team
    - and some of them are managers of that team

> **The Roles**: How this is managed depends on many things. Sometimes roles will appear as part of the `roles` claim on their identity token. In other words, centralized identity will maintain that. Other times it will be maintained by keeping a list of employees, and tracking roles within the app itself.


## The Catalog Items

### Find a Vendor

### Get a List of Catalog Items For That Vendor 


### Get All Catalog Items


- if that vendor doesn't exist, return a 404

### Add A Catalog Item

- Must be a member of the software team

