$(function () {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("form[name='registration']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            txtname: { required: true, minlength:3 },
            txtusername: "required",
            txtemailid: {
                required: true,
                // Specify that email should be validated
                // by the built-in "email" rule
                email: true
            },
            txtpassword: {
                required: true,
                minlength: 8
            }
        },
        txtphone: { required: true, number:true },
        // Specify validation error messages
        messages: {
            txtname: "Please enter your Name",
            txtusername: "Username cannot be left blank",
            password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 8 characters long"
            },
            email: "Please enter a valid email address",
            txtphone:"Please enter your contact number"
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });
});