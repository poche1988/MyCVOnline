
var QualificationsController = function (qualificationsServices) { // qualificationsServices must be in camel case
    var $btn;
    var id;
    
    var init = function (container) {
       
        $(container).on("click", ".js-delete-qualification", deletequalification)
        $(container).on("click", ".js-toggle-highlight", toggleHighlight) // click in toggle in qualifications section
        $(container).on("click", ".js-toggle-highlight-front", toggleHighlightFront) // click in toggle in index
    }

    var deletequalification = function () {
    
        $btn = $(this);
        id = $btn.data('id');
        var dialog = bootbox.dialog({
            message: 'Are you sure you want to delete this Qualification?',
            title: "Confirm",
            buttons:
                {
                    no: {
                        label: "No",
                        className: "btn-default",
                        callback: function ()
                        { bootbox.hideAll() }
                    },
                    yes: {
                        label: "Yes",
                        className: "btn-danger",
                        callback: function (result) {
                            qualificationsServices.removeQualification(id, deletesuccess, errorMessage)
                        }
                    }
                }
        });
    }

    var deletesuccess = function () {
        $btn.closest(".educationItem").fadeOut(800, function () {
            $btn.closest("li").remove();
        });
    }

    var toggleHighlight = function () {
        $btn = $(this);
        id = $btn.data('id')
        qualificationsServices.highlightqualification(id, Highlightsuccess, errorMessage)
    }

    var Highlightsuccess = function () {
        var text = ($btn.text() == "Highlighted!") ? "Highlighted!" : "Highlight";
        $btn.toggleClass("btn-info").toggleClass("btn-default").text(text);
        $('#' + id + '').toggleClass("HighlitedQualificationFrame").toggleClass("qualificationFrame");
    }

    var toggleHighlightFront = function () {
        $btn = $(this);
        id = $btn.data('id')
        qualificationsServices.highlightqualification(id, HighlightFrontsuccess, errorMessage)
    }

    var HighlightFrontsuccess = function () {
        $btn.toggleClass("highlightedQualification").toggleClass("highlightQualification");
        $btn.parent().parent().toggleClass("HighlitedQualificationFrame").toggleClass("qualificationFrame");
    }

    var errorMessage = function () {
        alert("something failed!")
    }

    return {
        init: init
    }
}(QualificationsServices); //IIFE INMEDIATLY INVOKE FUNCTION EXPRESSION

