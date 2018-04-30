var SectionServices = function () {
    var removeSection = function (id, deletesuccess, errorMessage) {
        $.ajax({
            url: "/api/Section/Delete/" + id,
            method: "POST",
            contentType: "aplication/Json; charset = UTF-8",
            DataType: "Json",
            success: (deletesuccess),
            error: (errorMessage)
        })
    }



    return {
        removeSection: removeSection,
    }
}();