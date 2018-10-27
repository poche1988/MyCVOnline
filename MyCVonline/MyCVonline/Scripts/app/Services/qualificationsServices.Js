var QualificationsServices = function () {
    var removeQualification = function (id, deletesuccess, errorMessage) {
        $.ajax({
            url: "/api/Qualifications/Delete/" + id,
            method: "POST",
            contentType: "aplication/Json; charset = UTF-8",
            DataType: "Json",
            success: (deletesuccess),
            error: (errorMessage)
        })
    }

    var highlightqualification = function (id, Highlightsuccess, errorMessage) {
        $.ajax({
            url: "/api/HighlightedQualification/Highlight/" + id,
            method: "POST",
            contentType: "aplication/Json; charset = UTF-8",
            DataType: "Json",
            success: (Highlightsuccess),
            error: (errorMessage)
        })
    }

    return {
        removeQualification: removeQualification,
        highlightqualification: highlightqualification
    }
}();