function getRulesControls() {
    // LockKeys();
    // LockPaste();
    // LockCopy();
    $("#txtImei").on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    $('#txtReferencePhone').keypress(function (event) {

        if (event.which < 48 || event.which > 57 || this.value.length === 20) {
            return false;
        }
    });

    $("#txtReferencePhone").on('paste', function (e) {
        e.preventDefault();

    });
    $('#txtPhone').keypress(function (event) {

        if (event.which < 48 || event.which > 57 || this.value.length === 20) {
            return false;
        }
    });

    $("#txtPhone").on('paste', function (e) {
        e.preventDefault();

    });

    $("#txtDocumentNumber").keypress(function (event) {
        var _documentType = $("#cbDocumentType").val(); // 0: DNI, 2: RUC

        if (_documentType == "0" || _documentType == "2") {
            if (event.which < 48 || event.which > 57 || this.value.length === 20) {
                return false;
            }
        }

    });
    $("#txtDocumentNumber").on('paste', function (e) {
        e.preventDefault();
    });

    $("#cbDocumentType").change(function () {
        $("#txtDocumentNumber").val("");
    });

    $.validator.addMethod(
                        "ValidateCac",
                        function (value, element) {

                            return (value == "" || value == "Seleccionar" || value == "-1") ? false : true;
                        },
                        "* Debe Seleccionar Punto de Atencion"
                         );
    $.validator.addMethod(
                       "ValidateMotivo",
                       function (value, element) {

                           return (value == "" || value == "Seleccionar" || value == "-1") ? false : true;
                       },
                       "* Debe Seleccionar un Motivo"
                        );
    $.validator.addMethod(
                      "ValidateTypeDoc",
                      function (value, element) {

                          return (value == "" || value == "Seleccionar" || value == "-1") ? false : true;
                      },
                      "* Debe Seleccionar un tipo de documento"
                       );
    $.validator.addMethod(
                      "ValidateRelation",
                      function (value, element) {

                          return (value == "" || value == "Seleccionar" || value == "-1") ? false : true;
                      },
                      "* Debe Seleccionar un tipo de Relacion "
                       );
    $.validator.addMethod(
                    "ValidateReference",
                    function (value, element) {

                        return (value.length >= 7 && value.length <= 20) ? true : false;
                    },
                    "* El rango de caracteres debe ser entre 7 y 20 "
                     );
    $.validator.addMethod(
                    "ValidatenmImei",
                    function (value, element) {

                        return /^([0-9]{15})*$/.test($("#txtImei").val()) ? true : false;
                    },
                    "* Formato incorrecto debe ser numerico con 15 caracteres "
                     );

    //reglas declaradas
    $('#formLock').validate({ // initialize the plugin
        rules: {

            nmImei: {
                required: true,
                ValidatenmImei: true
            },
            nmddlMotive: {
                ValidateMotivo: true,
                required: true
            },
            nmCACDAC: {
                ValidateCac: true,
                required: true
            },
            nmFirstName: {

                required: true
            },
            nmLastName: {

                required: true
            },

            nmDocumentType: {
                ValidateTypeDoc: true,
                required: true
            },
            nmDocumentNumber: {

                required: true
            },
            nmTitularRelation: {
                ValidateRelation: true,
                required: true
            },
            nmOtherRelation: {

                required: true
            },
            nmPhone: {

                required: true
            },
            nmParient: {

                required: true
            },
            //nmCorreo: {
            //    ValidateEmail: true,
            //    required: true
            //},
            nmNotes: {
                required: true
            },
            nmReferencePhone: {
                required: true,
                ValidateReference: true
            }

        }, highlight: function (element) {
            $(element).closest('.error-input').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.error-input').removeClass('has-error');
        },
        messages: {
            nmImei: {
                required: "* El campo es Requerido"
            },
            nmddlMotive: {
                required: "* La opcion es Requerida"
            },
            nmCACDAC: {
                required: "* La opcion es Requerida"
            },
            nmDocumentType: {
                required: "* La opcion es Requerida"
            },
            nmTitularRelation: {
                required: "* La opcion es Requerida"
            },
            nmFirstName: {
                required: "*  El campo es requerido"
            },
            nmLastName: {
                required: "*  El campo es requerido"
            },
            nmDocumentNumber: {
                required: "*  El campo es requerido"
            },
            nmOtherRelation: {
                required: "*  El campo es requerido"
            },
            nmPhone: {
                required: "*  El campo es requerido"
            },
            nmParient: {
                required: "*  El campo es requerido"
            },
            //nmCorreo: {
            //    required: "*  El campo es requerido"
            //},
            nmNotes: {
                required: "*  El campo es requerido"
            },
            nmReferencePhone: {
                required: "*  El campo es requerido"
            }

        }

    });

}

function LockKeys() {

    $("#txtImei").keydown(function () {
        return false;
    });
    $('#txtImei').keypress(function (tecla) {
        return false;
    });
}
function LockPaste() {
    $("#txtImei").on('paste', function (e) {
        e.preventDefault();
        alert('Esta acción está prohibida');
    })


}
function LockCopy() {


    $("#txtImei").on('copy', function (e) {
        e.preventDefault();
        alert('Esta acción está prohibida');
    })
}
