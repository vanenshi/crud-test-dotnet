Feature: Create List Edit Delete Customer


    Background:
        Given system error codes are following
          | Code | Description                                                |
          | 101  | Invalid Mobile Number                                      |
          | 102  | Invalid Email address                                      |
          | 103  | Invalid Bank Account Number                                |
          | 201  | Duplicate customer by First-name, Last-name, Date-of-Birth |
          | 202  | Duplicate customer by Email address                        |

    @ignore
    Scenario: User Creates, List, Edit, Delete a Customer
        Given the platform has "0" customers
		    When user creates a customer with following data by sending 'Create Customer Command' through API
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |
        Then the user should receive "success" message
        And get customer all customers should return "1" customer with below filter
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |
        When user creates a customer with following data by sending 'Create Customer Command' through API
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |
        Then the user should receive "failed" message with error codes
          | Code |
          | 201  |
          | 202  |
        And user can request to get all customers and get "1" records
        When user edit customer by email "john@doe.com" with new data
          | FirstName | LastName | Email            | PhoneNumber | DateOfBirth | BankAccountNumber |
          | Jane      | William  | jane@william.com | +3161234567 | 01-FEB-2010 | IR000000000000002 |
        Then the user should receive "success" message
        And get customer all customers should return "0" customer with below filter
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |
        And get customer all customers should return "1" customer with below filter
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | Jane      | William  | jane@william.com | +3161234567 | 01-FEB-2010 | IR000000000000002 |
        When user delete customer by email "jane@william.com"
        Then the user should receive "success" message
        And the user get all customers endpoint should return "0" records