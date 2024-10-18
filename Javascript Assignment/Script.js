document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("auth-form");
    const formTitle = document.getElementById("form-title");
    const toggleLink = document.getElementById("toggle-link");
    let isSignUp = true;
  
    toggleLink.addEventListener("click", () => {
      isSignUp = !isSignUp;
      formTitle.textContent = isSignUp ? "Sign Up" : "Login";
      form.querySelector("button").textContent = isSignUp ? "Sign Up" : "Login";
      
      // Clear the form inputs
      form.reset();
  
      // Update the toggle link text
      toggleLink.textContent = isSignUp
        ? "Already have an account? Login"
        : "Don't have an account? Sign Up";
  
      // Show/hide fields based on the form type
      if (isSignUp) {
        addSignUpFields();
      } else {
        addLoginFields();
      }
    });
  
    const addSignUpFields = () => {
      form.innerHTML = `
        <input type="text" id="name" placeholder="Name" required>
        <input type="text" id="username" placeholder="Username" required>
        <input type="password" id="password" placeholder="Password" required>
        <input type="password" id="confirm-password" placeholder="Confirm Password" required>
        <button type="submit">Sign Up</button>
      `;
    };
  
    const addLoginFields = () => {
      form.innerHTML = `
        <input type="text" id="username" placeholder="Username" required>
        <input type="password" id="password" placeholder="Password" required>
        <button type="submit">Login</button>
      `;
    };
  
    // Initialize the form with Sign Up fields
    addSignUpFields();
  
    form.addEventListener("submit", (event) => {
      event.preventDefault();
  
      const username = document.getElementById("username").value;
      const password = document.getElementById("password").value;
  
      if (isSignUp) {
        const name = document.getElementById("name").value;
        const confirmPassword = document.getElementById("confirm-password").value;
  
        // Validate that all fields are filled
        if (username === "" || password === "" || name === "" || confirmPassword === "") {
          alert("Please fill in all fields.");
          return;
        }
  
        // Check if passwords match
        if (password !== confirmPassword) {
          alert("Passwords do not match.");
          return;
        }
  
        // Check if user already exists
        if (localStorage.getItem(username)) {
          alert("User already exists!");
          return;
        }
  
        // Save user data to local storage
        const userData = {
          name: name,
          username: username,
          password: password,
        };
        localStorage.setItem(username, JSON.stringify(userData));
        alert("Sign Up successful! You can now login.");
  
        // Switch to login form
        toggleLink.click();
      } else {
        // Validate login credentials
        const storedUserData = localStorage.getItem(username);
        if (!storedUserData) {
          alert("User not found!");
          return;
        }
  
        const parsedUserData = JSON.parse(storedUserData);
        if (password === parsedUserData.password) {
          alert("Login successful! Welcome, " + parsedUserData.name + ".");
        } else {
          alert("Invalid username or password.");
        }
      }
    });
  });
  