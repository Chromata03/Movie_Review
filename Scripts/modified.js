function validate_logout() {
    if (confirm("Do you really want to logout?")) {
        return true;
    }
    return false;
}
function validate() {
    var password = document.getElementById('passwordField').value;
    var confirmPassword = document.getElementById('confirmPasswordField').value;
    var lengthValid = password.length >= 8;
    var letterValid = /[a-zA-Z]/.test(password);
    var numValid = /[0-9]/.test(password);
    var specialCharValid = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password);
    //Matching Check
    if (password !== confirmPassword) {
        document.getElementById('passwordMatchError').innerText = "Password does not match!";
        return false;
        alert("FAILED!");
    }
    //Complexity Check
    if (!lengthValid || !letterValid || !numValid || !specialCharValid) {
        document.getElementById('passwordMatchError').innerText = "Password does not meet complexity requirements.";
        return false;
        alert("FAILED(2)!");
    }
    return true;
}

function complexity(event) {
    var password = event.target.value;
    var lengthValid = password.length >= 8;
    var letterValid = /[a-zA-Z]/.test(password);
    var numValid = /[0-9]/.test(password);
    var specialCharValid = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password);

    var check = document.getElementById('passwordComplexityContainer');
    check.style.display = lengthValid && letterValid && numValid && specialCharValid ? 'none' : 'block';

    document.getElementById('passwordLength').classList.toggle('valid', lengthValid);
    document.getElementById('PasswordLetterChar').classList.toggle('valid', letterValid);
    document.getElementById('PasswordNumChar').classList.toggle('valid', numValid);
    document.getElementById('passwordSpecialChar').classList.toggle('valid', specialCharValid);
}

function hideCheck() {
    $('#passwordComplexityContainer').hide();
    $('#matchMsgContainer').hide();
}

$(document).ready(function() {
    $('#btnUpdateInfo').click(function (event) {
        $("#userUpdates").toggle();
        $("#updateInfo").toggle();
    });
});

$(document).ready(function () {
    $('#btnUpdatePass').click(function (event) {
        $("#updatePass").toggle();
    });
});

function validateMatch(event) {
    var confirmPassword = event.target.value;
    var password = document.getElementById(passwordFieldId).value;
    console.log(confirmPassword + " " + password);
    try {
        // Matching Check
        document.getElementById('matchMsgContainer').style.display = 'block';
        if (password !== confirmPassword) {
            document.getElementById('matchMsg').innerText = "Password does not match!";
        } else {
            document.getElementById('matchMsg').innerText = "Password match!";
            document.getElementById('matchMsg').classList.toggle('valid', password == confirmPassword);
        }
    } catch (e) {
    }
}