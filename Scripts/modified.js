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

function validateMatch(event) {
    var confirmPassword = event.target.value;
    var password = document.getElementById(passwordFieldId).value;
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


$(document).ready(function() {
    $('#btnUpdateInfo').click(function(event) {
        $("#updateInfoContainer, #updateInfo").toggle();
        $(" #updateInfo").css("display", "block");
        $("#updatePass").hide();
        $("#btnUpdatePass").prop("disabled", !$("#btnUpdatePass").prop("disabled")); //disable other button
    });

    $('#btnUpdatePass').click(function(event) {
        $("#updateInfoContainer, #updatePass").toggle();
        $(" #updatePass").css("display", "block");
        $("#updateInfo").hide();
        $("#btnUpdateInfo").prop("disabled", !$("#btnUpdateInfo").prop("disabled")); //disable other button
    });
});

$(document).ready(function() {
    $('#saveUpdate').click(function (event) {
        if ($('#updatePass').css("display") == "block") {
            changePassword();
            return;
        }
        if ($('#updateInfo').css("display") == "block") {
            var data = {};

            $('#changeName').click(function (event) {
                changeDNameFld.prop("disabled", "false");
            });
            $('#changeEmail').click(function (event) {
                changeEmailFld.prop("disabled", "false");
            });

            if ($('#' + changeDNameFld).val() !== '') {
                data.name = $('#' + changeDNameFld).val();
            }
            if ($('#' + changeEmailFld).val() !== '') {
                data.email = $('#' + changeEmailFld).val();
            }

            changeDetails(data);
            return;
        }
    });
});

function changePassword() {
    event.preventDefault();

    var dataToSave = {
        curPass: $('#' + curPassFld).val(),
        newPass: $('#' + passwordFieldId).val(),
        conPass: $('#' + conNewPassFld).val()
    };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Profile.aspx/changePassword",
        data: JSON.stringify(dataToSave),
        dataType: "json",
        success: function (response) {
            var responseData = JSON.parse(response.d);
            if (responseData.success) {
                console.log("Success!");
                window.location.href = "Login.aspx";
            }
            else {
                $('#headerContainer').attr("visible", "true");
                $('#headerMsg').text(responseData.message);
            }
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}

function changeDetails(data) {
    event.preventDefault();

    var dataToSave = data;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Profile.aspx/changeDetails",
        data: JSON.stringify(dataToSave),
        dataType: "json",
        success: function (response) {
            var responseData = JSON.parse(response.d);
            $('#headerContainer').attr("visible", "true");
            $('#headerMsg').text(responseData.message);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}

$(document).ready(function () {
    $("#showButton").click(function () {
        var entryContent = $('#entry-content');

        // Toggle class to handle content visibility
        entryContent.toggleClass('expanded');

        // Update button text based on content visibility
        if (entryContent.hasClass('expanded')) {
            $(this).text('Show Less');
        } else {
            $(this).text('Show More');
            entryContent.scrollTop(0);
        }

    });
});

//Handling of Comments
$(document).ready(function () {
    $('#submit-comment').submit(function (event) {
        event.preventDefault();

        // Get the value from the comment input
        var commentText = $('#commentInput').val();

        // Create a new comment element
        var newComment = $('<div class="comment"></div>').text(commentText);

        // Append the new comment to the comments container
        $('.comments').append(newComment);

        // Clear the comment input
        $('#commentInput').val('');
    });
});