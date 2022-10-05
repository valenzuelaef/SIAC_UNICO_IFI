function getRulesControls() {
    LockKeys();
    LockPaste();
    LockCopy();
    ClearField();
    convertMayus();
    $("#txtReferencePhone").on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
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
                   "ValidateJefeSix",
                   function (value, element) {
                       var patt = new RegExp(/^[a-zA-Z][0-9]+$/);
                       var res = patt.test(value);
					 var intMaxBoss= Number($("#hdkeyMaxCharactersBoss").val());
                       return (value.length == intMaxBoss && res) ? true : false;
                   },
                   $("#hdkeyMaxCharactersBossMsg").val()
                    );



    //reglas declaradas
    $('#formUnLock').validate({ // initialize the plugin
        rules: {

            nmImei: {
                required: true

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

            nmNotes: {
                required: true
            },
            nmReferencePhone: {
                required: true,
                ValidateReference: true,

            },
            nmBoss: {
                required: true,
                ValidateJefeSix: true,

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
            },
            nmBoss: {
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
    $("#txtDocumentNumber").keypress(function (event) {
        var _documentType = $("#cbDocumentType").val(); // 0: DNI, 2: RUC
        if (_documentType == "0" || _documentType == "2") {
            if (event.which < 48 || event.which > 57 || this.value.length === 20) {
                return false;
            }
        }
    });
}
function ClearField() {
    $("#cbDocumentType").change(function () {
        $("#txtDocumentNumber").val("");
    });
}
function LockPaste() {
    $("#txtImei").on('paste', function (e) {
        e.preventDefault();
        alert('Esta acción está prohibida');
    })
    $("#txtDocumentNumber").on('paste', function (e) {
        e.preventDefault();
    })

}
function LockCopy() {


    $("#txtImei").on('copy', function (e) {
        e.preventDefault();
        alert('Esta acción está prohibida');
    })
}


function convertMayus()
{
    $(document).ready(function () {
        $("input[name=nmBoss]").on("keypress", function () {
            $input = $(this);
            setTimeout(function () {
                $input.val($input.val().toUpperCase());
            }, 50);
        })
    })
}