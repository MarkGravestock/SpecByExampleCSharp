#### Quick Experiment to see how much overhead ATDD/SBE Framework adds

- Solution not ATDDed, utilised example implementation, but wanted to understand how using such a framework may impede/assist.
- Should try as a ATDD exercise for other requirements
- Ultimately the power in SbE as a practice is the conversation with the customer not the technical implementation.
    - This is well covered in the supporting video

### Specification by Example Patterns used

- Illustrating using examples / Refining the specification
    - Spreadsheet contains key example
    - Should we remove duplication in examples e.g. 2 examples have same implementation 
- Automating validation without changing specification
    - Fidelity maintained between examples in spreadsheet and code?

### Things to consider when choosing approach

- Certain tools may support customer more customer friendly example formats e.g. https://concordion.org/ supports SbE
- Depends on customer interaction over the longer term
    - Will customers actually return to these tests over time?
    - Do they provide value as residual documentation? E.g. HTML allows rules/note to be in plain english, support tabular data directly
         - Is it core differentiating logic, it may be worth maintaining a limited set? Part of 'Living Documentation' pattern where we
         distill the transient artifacts into residual documentation.
- Depends on level of technical artifacts user/customer will tolerate when reviewing 
- Decision depend on level of complexity of feature and what the type of ATDD test - is it at unit test level or part of an integration test?

### Other considerations

Not the key point of the example.... some refactoring possible

- Primitive Obsession 

### NB 

By default Concordion outputs HTML results to %TEMP%, have added .config file to force it to the build directory 
for the test project. 