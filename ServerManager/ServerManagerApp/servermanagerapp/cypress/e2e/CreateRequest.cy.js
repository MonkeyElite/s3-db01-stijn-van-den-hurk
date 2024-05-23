describe("template spec", () => {
  it("Visits Create Page", () => {
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
});
