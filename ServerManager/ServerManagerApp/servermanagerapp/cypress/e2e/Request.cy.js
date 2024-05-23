describe("template spec", () => {
  it("Creates Request Page", () => {
    cy.visit("http://localhost:3000/request");

    // Wait for the page to load fully
    cy.contains("Create new request").should("be.visible");

    // Click the 'Create new request' button using data-testid
    cy.get('[data-testid="create-request-button"]').click();

    // Verify that the navigation was successful (e.g., URL change)
    cy.url().should("include", "/request/create");

    // Wait for the create request form to load
    cy.get('[data-testid="create-request-title"]').should("be.visible");

    // Enter title in the create request form
    cy.get('[data-testid="create-request-title"]').type("Test Request Title");

    // Enter description in the create request form
    cy.get('[data-testid="create-request-description"]').type(
      "This is a test description for the request."
    );

    // Click the submit button
    cy.get('[data-testid="create-request-submit"]').click();

    // Verify that the navigation was successful (e.g., URL change)
    cy.url().should("include", "/request");
  });

  it("Updates Request", () => {
    cy.visit("http://localhost:3000/request");

    // Find the RequestItem with the title "Test Request Title"
    cy.contains(".text-white", "Test Request Title")
      .parent(".aspect-square")
      .within(() => {
        // Perform actions on the found RequestItem, e.g., click update button
        cy.get(".bg-blue-500").click(); // Assuming the update button has class bg-blue-500
      });

    // Optionally, assert that the URL changes or perform further actions/assertions
    cy.url().should("include", "/request/update/");

    // Enter description in the update request form
    cy.get('[data-testid="update-request-description"]').type(
      "This was a test description for the request."
    );

    // Click the submit button
    cy.get('[data-testid="update-request-submit"]').click();

    // Verify that the navigation was successful (e.g., URL change)
    cy.url().should("include", "/request");
  });

  it("Deletes Request", () => {
    cy.visit("http://localhost:3000/request");

    // Find the RequestItem with the title "Test Request Title"
    cy.contains(".text-white", "Test Request Title")
      .parent(".aspect-square")
      .within(() => {
        // Click on the delete button (red trash icon)
        cy.get(".bg-red-500").click(); // Assuming the delete button has class bg-red-500
      });

    // Optionally, assert that the item has been deleted
    // For example, verify that the item no longer exists in the DOM
    cy.contains(".text-white", "Test Request Title").should("not.exist");
  });
});
