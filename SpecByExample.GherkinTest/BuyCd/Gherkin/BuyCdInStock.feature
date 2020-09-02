Feature: Buy CD
  
  Scenario: CD is in stock and not in top 100
    Given a CD that's not in the Top 100 
    And we have it in stock
    And the customer's card payment will be accepted
    When The customer buys that CD priced at £9.99
    Then One copy is deducted from CD's stock
    And The customer's card is charged our price £9.99 for that CD
    And The charts are notified of the sale