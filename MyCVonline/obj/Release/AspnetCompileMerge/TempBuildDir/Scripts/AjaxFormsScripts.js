//Ajax On Success
function profileUpdated(response) {
    $("#name").html(response.Name);
    $("#profession").html(response.Profession);
    $("#abstract").html(response.Summary);
    $("#address").html(response.Address);

    if (!!response.Address) {
        $("#addressIcon").css("display", "inline-block");
        $("#address").css("display", "inline-block");
    }
    else {
        $("#addressIcon").css("display", "none");
        $("#address").css("display", "none");
    }

    $("#phone").html(response.PhoneNumber);

    if (!!response.PhoneNumber) {
        $("#phoneIcon").css("display", "inline-block");
        $("#phone").css("display", "inline-block");
    }
    else {
        $("#phoneIcon").css("display", "none");
        $("#phone").css("display", "none");
    }



    if (response.success)
        $('.Form').slideUp('3000');

}

function qualificationUpdated(response) {
    if (response.Errors != null) {
        $('#QualificationUpdate [data-valmsg-for="To"]').text(response.Errors[0].Error);
        $('#QualificationUpdate [data-valmsg-for="To"]').addClass("field-validation-error");

    }

    if (response.success) {
        $("#QualificationsList").html("");
        var html = "";
        var locale = "en-us";
        for (var key in response.Education) {
            if (response.Education.hasOwnProperty(key)) {

                //div wraper
                if (response.Education[key].Highlighted)
                    html += '<div class="HighlitedQualificationFrame educationItem">';
                else
                    html += '<div class="qualificationFrame educationItem">';

                //Education Title
                html += '<div class="degree">';
                html += (response.Education[key].QualificationTitle);
                html += '</div>';

                //Education Dates and level
                html += '<span>';
                var dateFrom = new Date(parseInt((response.Education[key].From).substr(6)));
                var month = dateFrom.toLocaleString(locale, { month: "short" });
                html += month;
                html += '-';
                html += dateFrom.getFullYear();
                html += ' to ';
                var dateTo = new Date(parseInt((response.Education[key].To).substr(6)));
                month = dateTo.toLocaleString(locale, { month: "short" });
                html += month;
                html += '-';
                html += dateTo.getFullYear();
                html += ' - ';
                html += response.Education[key].level.LevelName;
                html += '</span>';


                //university
                html += '<span class="University">';
                html += response.Education[key].University;
                html += '</span>';

                //actions
                html += '<div class="actions actionsQualification">';

                //edit button
                html += '<a class="editQualificationIcon"';
                html += ' data-id="' + response.Education[key].QualificationId;
                html += '" data-title="' + response.Education[key].QualificationTitle;
                html += '" data-university="' + response.Education[key].University;
                html += '" data-from="';
                html += convertDate(dateFrom);
                html += '" data-to="';
                html += convertDate(dateTo);
                html += '" data-level="' + response.Education[key].level.QualificationLevelId;
                html += '"> <span class="glyphicon glyphicon-pencil"></span>';
                html += '</a>';

                //delete button
                html += '<a class="deleteQualificationIcon js-delete-qualification"';
                html += ' data-id="' + response.Education[key].QualificationId;
                html += '"> <span class="glyphicon glyphicon-trash"></span>';
                html += '</a>';

                //Highlight Button
                if (response.Education[key].Highlighted)
                    html += '<a class="highlightedQualification js-toggle-highlight-front"';
                else
                    html += '<a class="highlightQualification js-toggle-highlight-front"';
                html += ' data-id="' + response.Education[key].QualificationId;
                html += '"> <span class="glyphicon glyphicon-check"></span>';
                html += '</a>';

                //closing actions
                html += '</div>';


                //closing div wraper
                html += '</div>';
            }
        }

        $("#QualificationsList").html(html);
        $('.editQualificationIcon').bind('click', formatQualificationForm); // add event listeners
        intialController();


        $('.Form').slideUp('3000');
        $.validator.unobtrusive.parse(this);
    }

}

function jobUpdated(response) {
    if (response.Errors != null) {
        $('#JobAjaxForm [data-valmsg-for="ToDate"]').text(response.Errors[0].Error);
        $('#JobAjaxForm [data-valmsg-for="ToDate"]').addClass("field-validation-error");

    }

    if (response.success) {
        $("#JobsList").html("");
        var html = "";
        var locale = "en-us";
        for (var key in response.Jobs) {
            if (response.Jobs.hasOwnProperty(key)) {
                //wraper
                html += '<div class="JobContainer JobItem">';

                //job title
                html += '<div class="JobTitle">';
                html += '<span>';
                html += response.Jobs[key].JobTitle + " - " + response.Jobs[key].CompanyName;
                html += '</span>';
                html += '</div>';

                //job dates
                html += '<div class="Jobdates">';
                html += '<span>';
                var dateFrom = new Date(parseInt((response.Jobs[key].From).substr(6)));
                var month = dateFrom.toLocaleString(locale, { month: "short" });
                html += month;
                html += '-';
                html += dateFrom.getFullYear();
                html += ' - ';

                if (response.Jobs[key].OnGoing)
                    html += 'Ongoing';
                else {
                    var dateTo = new Date(parseInt((response.Jobs[key].To).substr(6)));
                    month = dateTo.toLocaleString(locale, { month: "short" });
                    html += month;
                    html += '-';
                    html += dateTo.getFullYear();

                }
                html += '</span>';
                html += '</div>';

                //job description
                html += '<div class="JobDescription">';
                html += '<p style="white-space: pre-line;">';
                html += response.Jobs[key].JobDescription;
                html += '</p>';
                html += '</div>';

                //references
                if (response.Jobs[key].Reference) {
                    html += '<div class="JobReferences">';
                    html += '<p>';
                    html += '<strong>References</strong><br>' + response.Jobs[key].Reference;
                    html += '</p>';
                    html += '</div>';
                }

                //actions
                html += '<div class="actions actionsJob" id="actionsJob">';

                //edit button

                html += '<a class="editJobIcon"';
                html += ' data-id="' + response.Jobs[key].JobId;
                html += '" data-title="' + response.Jobs[key].JobTitle;
                html += '" data-company="' + response.Jobs[key].CompanyName;
                html += '" data-ongoing="' + response.Jobs[key].OnGoing;
                html += '" data-checked="' + response.Jobs[key].OnGoing;
                html += '" data-from="';
                html += convertDate(dateFrom);
                html += '" data-to="';
                if (response.Jobs[key].OnGoing) {
                    html += convertDate(new Date(Date.now()));
                }
                else
                    html += convertDate(dateTo);
                html += '" data-description="' + response.Jobs[key].JobDescription;
                html += '" data-reference="' + response.Jobs[key].Reference;
                html += '"> <span class="glyphicon glyphicon-pencil"></span>';
                html += '</a>';

                //delete button
                html += '<a class="deleteJobIcon js-delete-job"';
                html += ' data-id="' + response.Jobs[key].JobId;
                html += '"> <span class="glyphicon glyphicon-trash"></span>';
                html += '</a>';

                //End actions
                html += '</div>';


                //closing div wraper
                html += '</div>';
            }
        }

        $("#JobsList").html(html);
        $('.editJobIcon').bind('click', formatJobForm); // add event listeners
        intialController();


        $('.Form').slideUp('3000');
        $.validator.unobtrusive.parse(this);
    }

}

function sectionUpdated(response) {
    if (response.success) {
        var html = "";
        $('#sectionsAboveEducationContainer').html("");
        $('#sectionsUnderEducationContainer').html("");
        $('#sectionsAboveJobsContainer').html("");
        $('#sectionsUnderJobsContainer').html("");

        for (var key in response.SectionsAboveEducation) {
            if (response.SectionsAboveEducation.hasOwnProperty(key)) {


                //wraper
                html += '<div class="sectionContainer">';

                //content
                html += '<div class ="TitleLeft">';
                html += response.SectionsAboveEducation[key].Title.toUpperCase();
                html += '</div>';
                html += '<div class ="SectionDescriptionLeft">';
                html += '<p style="white-space:pre-line;">';
                html += response.SectionsAboveEducation[key].Description;
                html += '</p>';
                html += '</div>';

                //actions
                html += '<div class ="actions actionsSection">';
                html += '<a class="editSectionIcon" ';
                html += 'data-id="' + response.SectionsAboveEducation[key].SectionId + '"';
                html += ' data-title="' + response.SectionsAboveEducation[key].Title + '"';
                html += ' data-description="' + response.SectionsAboveEducation[key].Description + '"';
                html += ' data-position="2">';
                html += '<span class="glyphicon glyphicon-pencil"></span>';
                html += '</a>';
                html += '<a class="deleteSectionIcon js-delete" data-id="' + response.SectionsAboveEducation[key].SectionId + '">';
                html += '<span class="glyphicon glyphicon-trash"></span>';
                html += '</a>';
                html += '</div>';

                //end wraper
                html += '</div>';

                $("#sectionsAboveEducationContainer").html(html);

            }
        }

        html = "";

        for (var key in response.SectionsUnderEducation) {
            if (response.SectionsUnderEducation.hasOwnProperty(key)) {

                //wraper
                html += '<div class="sectionContainer">';

                //content
                html += '<div class ="TitleLeft">';
                html += response.SectionsUnderEducation[key].Title.toUpperCase();
                html += '</div>';
                html += '<div class ="SectionDescriptionLeft">';
                html += '<p style="white-space:pre-line;">';
                html += response.SectionsUnderEducation[key].Description;
                html += '</p>';
                html += '</div>';

                //actions
                html += '<div class ="actions actionsSection">';
                html += '<a class="editSectionIcon" ';
                html += ' data-id="' + response.SectionsUnderEducation[key].SectionId + '"';
                html += ' data-title="' + response.SectionsUnderEducation[key].Title + '"';
                html += ' data-description="' + response.SectionsUnderEducation[key].Description + '"';
                html += ' data-position="1">';
                html += '<span class="glyphicon glyphicon-pencil"></span>';
                html += '</a>';
                html += '<a class="deleteSectionIcon js-delete" data-id="' + response.SectionsUnderEducation[key].SectionId + '">';
                html += '<span class="glyphicon glyphicon-trash"></span>';
                html += '</a>';
                html += '</div>';

                //end wraper
                html += '</div>';

                $("#sectionsUnderEducationContainer").html(html);



            }
        }

        html = "";

        for (var key in response.SectionsAboveJobs) {
            if (response.SectionsAboveJobs.hasOwnProperty(key)) {

                //wraper
                html += '<div class="sectionContainer">';

                //content
                html += '<div class ="Title">';
                html += '<span><strong>';
                html += response.SectionsAboveJobs[key].Title.toUpperCase();
                html += '</strong></span>';
                html += '</div>';
                html += '<div class ="sectionDescription">';
                html += '<p style="white-space:pre-line;">';
                html += response.SectionsAboveJobs[key].Description;
                html += '</p>';
                html += '</div>';

                //actions
                html += '<div class ="actions actionsSection">';
                html += '<a class="editSectionIcon" ';
                html += 'data-id="' + response.SectionsAboveJobs[key].SectionId + '"';
                html += ' data-title="' + response.SectionsAboveJobs[key].Title + '"';
                html += ' data-description="' + response.SectionsAboveJobs[key].Description + '"';
                html += ' data-position="4">';
                html += '<span class="glyphicon glyphicon-pencil"></span>';
                html += '</a>';
                html += '<a class="deleteSectionIcon js-delete" data-id="' + response.SectionsAboveJobs[key].SectionId + '">';
                html += '<span class="glyphicon glyphicon-trash"></span>';
                html += '</a>';
                html += '</div>';

                //end wraper
                html += '</div>';

                $("#sectionsAboveJobsContainer").html(html);


            }
        }

        html = "";

        for (var key in response.SectionsUnderJobs) {
            if (response.SectionsUnderJobs.hasOwnProperty(key)) {

                //wraper
                html += '<div class="sectionContainer">';


                //content
                html += '<div class ="Title">';
                html += '<span><strong>';
                html += response.SectionsUnderJobs[key].Title.toUpperCase();
                html += '</strong></span>';
                html += '</div>';
                html += '<div class ="sectionDescription">';
                html += '<p style="white-space:pre-line;">';
                html += response.SectionsUnderJobs[key].Description;
                html += '</p>';
                html += '</div>';

                //actions
                html += '<div class ="actions actionsSection">';
                html += '<a class="editSectionIcon" ';
                html += 'data-id="' + response.SectionsUnderJobs[key].SectionId + '"';
                html += ' data-title="' + response.SectionsUnderJobs[key].Title + '"';
                html += ' data-description="' + response.SectionsUnderJobs[key].Description + '"';
                html += ' data-position="3">';
                html += '<span class="glyphicon glyphicon-pencil"></span>';
                html += '</a>';
                html += '<a class="deleteSectionIcon js-delete" data-id="' + response.SectionsUnderJobs[key].SectionId + '">';
                html += '<span class="glyphicon glyphicon-trash"></span>';
                html += '</a>';
                html += '</div>';

                //end wraper
                html += '</div>';

                $("#sectionsUnderJobsContainer").html(html);


            }
        }

        $('.editSectionIcon').bind('click', formatSectionForm); // add event listeners
        intialController();


        $('.Form').slideUp('3000');

    }

};

//events
$('.editProfile').click(function () {
    $('.PersonalInfoForm').slideDown('3000');
});

$('.editPhotoIcon').click(function () {
    $('.fotoForm').slideDown('3000');
});

$('#addQualificationIcon').click(function () {
    resetQualificationForm();

    $('#qualificationFormTitle').html("Add a Qualification");

    $('.QualificationForm').slideDown('3000');
});

$('#addJobIcon').click(function () {
    resetJobForm();

    $("h4.JobFormTitle").text("Add a Job");

    $('.JobForm').slideDown('3000');
});

$('.addSectionIcon').click(function () {
    resetSectionForm();

    $("#sectionFormTitle").text("Add a Section");

    $('#PositionId').get(0).selectedIndex = $(this).data('position');

    $('.SectionForm').slideDown('3000');
});



$('.editQualificationIcon').click(formatQualificationForm);

$('.editJobIcon').click(formatJobForm);

$('.editSectionIcon').click(formatSectionForm);

$('.close').click(function () {
    $('.Form').slideUp('3000');
});


//formating forms
function formatQualificationForm() {
    resetQualificationForm();
    $btn = $(this);
    $('#qualificationFormTitle').html("Edit Qualification");

    $('#ID').val($btn.data('id'));
    $('#Qualification').val($btn.data('title'));
    $('#University').val($btn.data('university'));
    $('#From').val($btn.data('from'));
    $('#To').val($btn.data('to'));
    $('#Level').get(0).selectedIndex = $btn.data('level');

    $('.QualificationForm').slideDown('3000');
    //ASIGNAR RESTO DE LOS VALORES

};

function formatJobForm() {
    resetJobForm();
    $btn = $(this);
    $("h4.JobFormTitle").text("Edit Job");

    $('#JobID').val($btn.data('id'));
    $('#JobTitle').val($btn.data('title'));
    $('#CompanyName').val($btn.data('company'));
    $('#JobDescription').val($btn.data('description'));
    $('#FromDate').val($btn.data('from'));
    if ($btn.data('ongoing') == "True" || $btn.attr("data-checked") == "true") {
        document.getElementById("OnGoing").checked = true;
    }
    else {
        document.getElementById("OnGoing").checked = false;
    }

    if ($('#OnGoing').is(":checked")) {
        $('#ToDate').hide();
    }
    else {
        $('#ToDate').show();
    }

    $('#ToDate').val($btn.data('to'));

    $('#OnGoing').val('true');

    $('#Reference').val($btn.data('reference'));

    $('.JobForm').slideDown('3000');
    //ASIGNAR RESTO DE LOS VALORES

};

function formatSectionForm() {
    resetSectionForm();
    $btn = $(this);
    $('#sectionFormTitle').html("Edit Section");

    $('#SectionId').val($btn.data('id'));
    $('#Title').val($btn.data('title'));
    $('#Description').val($btn.data('description'));
    $('#PositionId').get(0).selectedIndex = $btn.data('position');

    $('.SectionForm').slideDown('3000');
};

function resetQualificationForm() {
    document.getElementById("QualificationUpdate").reset();
    $('.field-validation-valid').html("");
    $(".error").removeClass("error");
    $('#ID').val(null);

}

function resetJobForm() {
    document.getElementById("JobAjaxForm").reset();

    $('.field-validation-valid').html("");
    $(".error").removeClass("error");
    $('#ToDate').show();
    $('#JobID').val(null);
}

function resetSectionForm() {
    document.getElementById("SectionAjaxForm").reset();

    $('.field-validation-valid').html("");
    $(".error").removeClass("error");
    $('#SectionId').val(null);
}

// functions
//update image preview container
function readfile(input) {
    if (input.files && input.files[0]) {
        validatePhoto(input);
    }
}

// validate image
function validatePhoto(input) {
    var validExtensions = ['jpg', 'png', 'JPG', 'PNG'];
    var fileName = input.files[0].name;
    var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
    if ($.inArray(fileNameExt, validExtensions) == -1) {
        input.type = ''
        input.type = 'file'

        $('#uploadPhoto').hide();
        $('#photoError').html("Only these file types are accepted : jpg, png");
        $('#image_preview').attr('src', '../Images/Users/DefaultUser.png');
    }
    if (input.files[0].size > 4 * 1024 * 1024) {
        $('#uploadPhoto').hide();
        $('#photoError').html("The image must be smaller than 4Mb");
        $('#image_preview').attr('src', '../Images/Users/DefaultUser.png');
    }
    else {
        $('#photoError').html("");
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image_preview').attr('src', e.target.result);
                $('#uploadPhoto').show(1000);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
}

//upload image with progress bar
(function () {
    var bar = $('.progress-bar');
    var percent = $('.progress-bar');
    var status = $('#status');

    $('#PhotoForm').ajaxForm({
        beforeSend: function () {
            status.empty();
            var percentVal = '0%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + '%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        success: function () {
            var percentVal = '100%';
            bar.width(percentVal)
            percent.html(percentVal);
            alert("success");
        },
        complete: function (xhr) {
            status.html(xhr.responseText);
            $('#photo').attr('src', document.getElementById("image_preview").getAttribute('src'));
           
            $('#photoContainer').removeClass('noFoto');
            $('#photo').removeClass('defaultPhoto');
            $('.Form').slideUp(1000);
            var percentVal = '0%';
            bar.width(percentVal)

        }
    });

})();

//format date for data-from and data-to
function convertDate(date) {
    var yyyy = date.getFullYear().toString();
    var mm = (date.getMonth() + 1).toString();
    var dd = date.getDate().toString();

    var mmChars = mm.split('');
    var ddChars = dd.split('');

    return yyyy + '-' + (mmChars[1] ? mm : "0" + mmChars[0]) + '-' + (ddChars[1] ? dd : "0" + ddChars[0]);
}

//ajax loading
function loadingAjax() {
    $('#loading').show();
};
function loadingCompletedAjax() {
    $('#loading').hide();
};

