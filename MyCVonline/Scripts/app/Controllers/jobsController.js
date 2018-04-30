
var JobsController = function (jobsServices) {
    var $btn;
    var id;

    var init = function (container) {
        $(container).on("click", ".js-delete-job", deletejob)
    }

    var deletejob = function () {
        $btn = $(this);
        id = $btn.data('id')
        var dialog = bootbox.dialog({
            message: 'Are you sure you want to delete this job?',
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
                            jobsServices.removeJob(id, deletesuccess, errorMessage)
                        }
                    }
                }
        });
    }

    var deletesuccess = function () {
        $btn.closest(".JobContainer").fadeOut(1000);
        $btn.closest(".jobItem").fadeOut(800, function () {
            $btn.closest("li").remove();
        });
    }

    var errorMessage = function () {
        alert("something failed!")
    }

    return {
        init: init
    }
}(JobsServices); //IIFE INMEDIATLY INVOKE FUNCTION EXPRESSION

