var SectionController = function (sectionServices) {
    var $btn;
    var id;

    var init = function (container) {
        
        $(container).on("click", ".js-delete", deletesection)
       
    }

    var deletesection = function () {
        $btn = $(this);
        id = $btn.data('id')
        var dialog = bootbox.dialog({
            message: 'Are you sure you want to delete this Section?',
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
                            sectionServices.removeSection(id, deletesuccess, errorMessage)
                        }
                    }
                }
        });
    }

    var deletesuccess = function () {
        $btn.closest(".sectionContainer").fadeOut(1000);
        $btn.closest(".sectionItem").fadeOut(800, function () {
            $btn.closest("li").remove();
        });
    }

    

    var errorMessage = function () {
        alert("something failed!")
    }

    return {
        init: init
    }
}(SectionServices); //IIFE INMEDIATLY INVOKE FUNCTION EXPRESSION