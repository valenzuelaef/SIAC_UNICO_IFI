
var TYPIFICATION = {
    ClaseId: "",
    SubClaseId: "",
    Tipo: "",
    ClaseDes: "",
    SubClaseDes: "",
    TipoId: "",
};
function CloseValidation(obj, pag, controls) {

    if (obj.hidAccion === 'G') {
        var sUser = obj.hidUserValidator;
        FC_GrabarCommit(pag, controls, obj.NamesUserValidator, obj.EmailUserValidator);
    }

    var mensaje;

    if (obj.hidAccion == 'F') {

        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';

        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
};

function noCopyMouse(e) {
    var isRight = (e.button) ? (e.button == 2) : (e.which == 3);

    if (isRight) {
        return false;
    }
    return true;
}

function FC_GrabarCommit(pag, controls, NamesUserValidator, EmailUserValidator) {
    document.getElementById('hidAccion').value = 'OK';
    $("#txtDateChangeMinor").attr('disabled', false);
    $("#txtCalendarDateChangeMinor").attr('disabled', false);

};

function validateDateMask(strDate) {
    if (mask(strDate, '##/##/####') != 1)
        return false;
    else
        return true;
};

function mask(InString, Mask) {
    LenStr = InString.length;
    LenMsk = Mask.length;
    if ((LenStr == 0) || (LenMsk == 0))
        return 0;
    if (LenStr != LenMsk)
        return 0;
    TempString = ""
    for (Count = 0; Count <= InString.length; Count++) {
        StrChar = InString.substring(Count, Count + 1);
        MskChar = Mask.substring(Count, Count + 1);
        if (MskChar == '#') {
            if (!isNumberChar(StrChar))
                return 0;
        }
        else if (MskChar == '?') {
            if (!isAlphabeticChar(StrChar))
                return 0;
        }
        else if (MskChar == '!') {
            if (!isNumOrChar(StrChar))
                return 0;
        }
        else if (MskChar == '*') {
        }
        else {
            if (MskChar != StrChar)
                return 0;
        }
    }
    return 1;
};

function isNumberChar(InString) {
    if (InString.length != 1)
        return (false);
    RefString = "1234567890";
    if (RefString.indexOf(InString, 0) == -1)
        return (false);
    return (true);
};

function getValue(id) {
    var c = document.getElementById(id);
    if (c != null) return Trim(c.value);
    return '';
};

function isNumber(pString) {
    var ok = "yes"; var temp;
    var valid = "0123456789";
    for (var i = 0; i < pString.length ; i++) {
        temp = "" + pString.substring(i, i + 1);
        if (valid.indexOf(temp) == "-1") ok = "no";
    }
    if (ok == "no") { return (false); } else { return (true); }

}

function getvalue(strData, intFieldNumber, separator) {
    var intCurrentField, intFoundPos, strValue, strNames;
    var bool = false;
    strNames = strData;
    intCurrentField = 0;
    while ((intCurrentField != intFieldNumber) && !bool) {
        intFoundPos = strNames.indexOf(separator);
        intCurrentField = intCurrentField + 1;
        if (intFoundPos != 0) {
            strValue = strNames.substring(0, intFoundPos);
            strNames = strNames.substring(intFoundPos + 1, strNames.length);
        }
        else {
            if (intCurrentField == intFieldNumber)
                strValue = strNames;
            else
                strValue = "";
            bool = true;
        }
    }
    if (strValue != "")
        return strValue;
    else
        return strNames;
};

function validarFecha(oControl) {
    var Day, Month, Year;
    var Fecha = document.getElementById(oControl);
    var valor = Fecha.value;
    var controlValida;
    controlValida = Fecha.id;

    if (validateDateMask(valor) == false) {
        alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)');
        return false;
    }

    Day = getvalue(valor, 1, "/");
    Month = getvalue(valor, 2, "/");
    Year = getvalue(valor, 3, "/");

    if ((isNumber(Day) && isNumber(Month) && isNumber(Year) && (Year.length == 4) && (Day.length <= 2) && (Month.length <= 2)) || ((Month == 2) && (Day <= 29))) {
        if ((Day != 0) && (Month != 0) && (Year != 0) && (Month <= 12) && (Day <= 31) && (Month != 2)) {

            if (Month == 4 || Month == 6 || Month == 9 || Month == 11) {
                if (Day > 30) {
                    alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)');
                    return false;
                }
            } else if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) {
                if (Day > 31) {
                    alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)');
                    return false;
                }
            }
            return true;
        }
        else if ((Month == 2) && (Day <= 29) && ((Year % 4) == 0) && ((Year % 100) != 0))
            return true;
        else if ((Month == 2) && (Day <= 29) && ((Year % 400) == 0))
            return true;
        else if ((Month == 2) && (Day <= 28))
            return true;
        else {
            if (Month > 12) {
                alert('El campo de mes debe ser como máximo 12.');
            }
            else if (Year.length != 4) {
                alert("El año debe tener 4 cifras.");
            }
            else if ((Month == 2) && (Day == 29) && ((Year % 4) == 0) && (Year % 100) == 0) {
                alert('Año no es bisiesto.');
            }
            else {
                alert('Fecha no válida');
            }
            if (Fecha.disabled == false)
                Fecha.focus();

            Fecha.select();
            return false;
        }
    }
    else {
        alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)');
        return false;
    }
};

function sololetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8 || tecla == 32) return true; //Tecla de retroceso (para poder borrar)
    patron = /[A-Za-z]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}



(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTChangeMinor.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , hidSupJef: $("#hidSupJef", $element)
            , cboCACDAC: $("#cboCACDAC", $element)
            , cboAccion: $("#cboAccion", $element)
            , cboPaisMod: $("#cboPaisMod", $element)
            , cboDepMod: $("#cboDepMod", $element)
            , cboProvinciaMod: $("#cboProvinciaMod", $element)
            , cboDistritoMod: $("#cboDistritoMod", $element)
            , cbocivilstatus: $("#cbocivilstatus", $element)
            , cboNacionalidadChangeMinor: $("#cboNacionalidadChangeMinor", $element)
            , hidCasoId: $("#hidCasoId",$element)
            //textbox
            , txtCargoDesem: $("#txtCargoDesem", $element)
            , txtNameComChangeMinor: $('#txtNameComChangeMinor', $element)
            , txtPhoneChangeMinor: $('#txtPhoneChangeMinor', $element)
            , txtContactocliChangeMinor: $('#txtContactocliChangeMinor', $element)
            , txtMovilChangeMinor: $('#txtMovilChangeMinor', $element)
            , txtMovil3ChangeMinor: $('#txtMovil3ChangeMinor', $element)
            , txtMovil4ChangeMinor: $('#txtMovil4ChangeMinor', $element)
            , txtDateChangeMinor: $('#txtDateChangeMinor', $element)
            , txtNumberChangeMinor: $('#txtNumberChangeMinor', $element)
            , txtEmailChangeMinor: $('#txtEmailChangeMinor', $element)
            , txtEmail2ChangeMinor: $('#txtEmail2ChangeMinor', $element)
            , txtEmail3ChangeMinor: $('#txtEmail3ChangeMinor', $element)
            , txtCalleChangeMinor: $('#txtCalleChangeMinor', $element)
            , txtEmail: $("#txtEmail", $element)
            , txtConfirmarEmailChangeMinor: $("#txtConfirmarEmailChangeMinor", $element)
            , txtNote: $('#txtNote', $element)

            //label : Datos cliente

            , lblContrato: $("#lblContrato", $element)
            , lblTipoCliente: $("#lblTipoCliente", $element)
            , lblContacto: $("#lblContacto", $element)
            , lblCodUbigeo: $("#lblCodUbigeo", $element)
            , lblCliente: $("#lblCliente", $element)
            , lblPlan: $("#lblPlan", $element)
            , lblFechaActivacion: $("#lblFechaActivacion", $element)
            , lblEstLinea: $("#lblEstLinea", $element)
            , lblTipAcu: $("#lblTipAcu", $element)
            , lblActivo: $("#lblActivo", $element)
            , lblDireccion: $("#lblDireccion", $element)
            , lblNotasDirec: $("#lblNotasDirec", $element)
            , lblDepartamento: $("#lblDepartamento", $element)
            , lblDistrito: $("#lblDistrito", $element)
            , lblCodPlano: $("#lblCodPlano", $element)
            , lblPais: $("#lblPais", $element)
            , lblProvincia: $("#lblProvincia", $element)
            , lblTipAcuerdo: $("#lblTipAcuerdo", $element)
            , lblErrorMessage: $("#lblErrorMessage", $element)
            , lblTitle: $('#lblTitle', $element)
            , lblCustomerID: $('#lblCustomerID', $element)
            , lblDNI_RUC: $('#lblDNI_RUC', $element)
            , lblRepren_Legal: $('#lblRepren_Legal', $element)
            , lblCicloFact: $('#lblCicloFact', $element)
            , lblLimiteCred: $('#lblLimiteCred', $element)
            //////////////////////////////////////////////////
            , lblNroTelefono: $('#lblNroTelefono', $element)
            , lblContacto: $('#lblContacto', $element)
            , lblCargDesemp: $('#lblCargDesemp', $element)
            , lblTelefono: $('#lblTelefono', $element)
            , lblCelular: $('#lblCelular', $element)
            , lblFax: $('#lblFax', $element)
            , lblEmail: $('#lblEmail', $element)
            , lblNomComercial: $('#lblNomComercial', $element)
            , lblContactoCli: $('#lblContactoCli', $element)
            , lblFechaNac: $('#lblFechaNac', $element)
            , lblNacionalidad: $('#lblNacionalidad', $element)
            , lblSexoCli: $('#lblSexoCli', $element)
            , lblEstadoCiv: $('#lblEstadoCiv', $element)
            //radiobutton 
            , rdbHombre: $("#rdbHombre", $element)
            , rdbMujer: $("#rdbMujer", $element)
            , rdbNA: $("#rdbNA", $element)
            , chkEmail: $("#chkEmail", $element)


            , btnCerrar: $("#btnCerrar", $element)
            , btnGuardar: $("#btnGuardar", $element)
            , btnConstancia: $("#btnConstancia", $element)
            , myModalLoad: $("#myModalLoad", $element)
            , divReglas: $("#divReglas", $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , txtCalendarDateChangeMinor: $("#txtCalendarDateChangeMinor", $element)


        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();

            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_click);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_click);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_Change);
            controls.txtDateChangeMinor.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateChangeMinor.addEvent(that, 'change', that.changeDateChangeMinor);
            controls.txtCalendarDateChangeMinor.addEvent(that, 'click', that.txtCalendarDateChangeMinor_click);
            controls.cboPaisMod.addEvent(that, 'change', that.InitValidaPais);
            controls.cboDepMod.addEvent(that, 'change', that.InitProvincias);
            controls.cboProvinciaMod.addEvent(that, 'change', that.InitDistritos);


            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },

        changeDateChangeMinor: function () {

            var that = this,
                controls = this.getControls();


        },

        InitValidaPais: function () {
         
            var that = this,
                controls = that.getControls();
            if (controls.cboPaisMod.val() == '154') {
                controls.cboDepMod.prop('disabled', false);
                that.InitDepartamentos();
            }
            else {
                controls.cboDepMod.val('0');
                controls.cboDepMod.prop('disabled', true);
                controls.cboProvinciaMod.val('0');
                controls.cboProvinciaMod.prop('disabled', true);
                controls.cboDistritoMod.val('0');
                controls.cboDistritoMod.prop('disabled', true);
               
            }
        },

        modelChangeMinor: {},
        render: function () {

            var that = this,
            control = that.getControls();
            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);
            control.divErrorAlert.hide();

            that.loadCustomerData();
            that.hidAccion = 'R';

            that.loadTypification();


            control.chkEmail.attr("checked", true);
            if (control.chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }
            that.getRulesControls();

        },

        getRulesControls: function () {

            //regla personalizada
            $.validator.addMethod(
                                 "ValidateEmail",
                                 function (value, element) {


                                     return $.IsEmail(value);

                                 },
                                 "* Ingrese un Correo Valido"
                                  );
            $.validator.addMethod(
                                 "ValidateEmail3",
                                 function (value, element) {

                                     if (value != "") {
                                         return $.IsEmail(value);
                                     }

                                     return true;

                                 },
                                  "* Ingrese un Correo Valido"
                                  );
            $.validator.addMethod(
                                  "ValidateEmail4",
                                  function (value, element) {

                                      if (value != "") {

                                          return $.IsEmail(value);
                                      }

                                      return true;
                                  },
                                 "* Ingrese un Correo Valido"
                                   );
            $.validator.addMethod(
                                 "ValidateEmail2",
                                 function (value, element) {

                                     var email = $("#txtEmailChangeMinor").val();
                                     var correo1 = email;
                                     var email2 = value;
                                     var correo2 = email2;


                                     return (correo1 != correo2) ? false : true;


                                 },
                                 "* No Coinciden Emails"
                                  );

            $.validator.addMethod(
                                "ValidateCac",
                                function (value, element) {

                                    return (value == "" || value == "Seleccionar") ? false : true;
                                },
                                "* Debe Seleccionar Punto de Atencion"
                                 );

            $.validator.addMethod(
                               "ValidateECiv",
                               function (value, element) {

                                   return (value == "" || value == "Seleccionar") ? false : true;
                               },
                               "* Debe Seleccionar un Estado Civil"
                                );

            $.validator.addMethod(
                               "ValidateNacionalidad",
                               function (value, element) {

                                   return (value == "" || value == "Seleccionar") ? false : true;
                               },
                               "* Debe Seleccionar una Nacionalidad"
                                );

            $.validator.addMethod(
                   "ValidateDep",
                   function (value, element) {

                       return (value == "" || value == "Seleccionar") ? false : true;
                   },
                   "* Debe Seleccionar Departamento"
                    );

            $.validator.addMethod(
                "ValidateProv",
                function (value, element) {

                    return (value == "" || value == "Seleccionar") ? false : true;
                },
                "* Debe Seleccionar Provincia"
                 );

            $.validator.addMethod(
                "ValidateDist",
                function (value, element) {

                    return (value == "" || value == "Seleccionar") ? false : true;
                },
                "* Debe Seleccionar Distrito"
                 );

            //reglas declaradas
            $('#frmChangeMinor').validate({ // initialize the plugin
                rules: {

                    nmtxtEmailChangeMinor: {
                        required: true,
                        ValidateEmail: true
                    },
                    nmtxtEmail2ChangeMinor: {
                        required: false,
                        ValidateEmail3: true
                    },
                    nmtxtEmail3ChangeMinor: {
                        required: false,
                        ValidateEmail4: true
                    },
                    nmtxtConfEmailChangeMinor: {
                        required: true,
                        ValidateEmail2: true
                    },
                    nmcboCACDAC: {
                        ValidateCac: true,
                        required: true
                    },
                    nmcbocivilstatus: {
                        ValidateECiv: true,
                        required: true
                    },
                    nmcboNacionalidadChangeMinor: {
                        ValidateNacionalidad: true,
                        required: true
                    },
                    nmcboDepMod: {
                        ValidateDep: true,
                        required: true
                    },
                    nmcboProvinciaMod: {
                        ValidateProv: true,
                        required: true
                    },
                    nmcboDistritoMod: {
                        ValidateDist: true,
                        required: true
                    },

                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {

                    nmtxtEmailChangeMinor: {
                        required: "* El campo es Requerido",


                    },
                    nmtxtConfEmailChangeMinor: {
                        required: "* No Coinciden Emails",


                    },

                    nmcboCACDAC: {
                        required: "* La opcion es Requerida",
                    },
                    nmcbocivilstatus: {
                        required: "* La opcion es Requerida",
                    },
                    nmcboNacionalidadChangeMinor: {
                        required: "* La opcion es Requerida",
                    },
                    nmcboDepMod: {
                        required: "*  Debe seleccionar departamento",
                    },
                    nmcboProvinciaMod: {
                        required: "* Debe seleccionar provincia.",
                    },
                    nmcboDistritoMod: {
                        required: "* Debe seleccionar distrito.",
                    }

                }

            });

        },
        loadTypification: function () {

            var obj = { strIdSession: Session.IDSESSION };
            $.app.ajax({
                type: 'POST',
                cache: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/IFITransactions/ChangeMinor/PageLoad',
                success: function (response) {

                  

                    if (response.data != null) {

                        TYPIFICATION.ClaseId = response.data.CLASE_CODE;
                        TYPIFICATION.SubClaseId = response.data.SUBCLASE_CODE;
                        TYPIFICATION.Tipo = response.data.TIPO;
                        TYPIFICATION.ClaseDes = response.data.CLASE;
                        TYPIFICATION.SubClaseDes = response.data.SUBCLASE;
                        TYPIFICATION.TipoId = response.data.TIPO_CODE;
                    }

                }

            });
        },

        txtCalendarDateChangeMinor_click: function () {
            var that = this,
                control = that.getControls();


        },



        windowAutoSize: function () {
            var hsize = Math.max(
                    document.documentElement.clientHeight,
                    document.body.scrollHeight,
                    document.documentElement.scrollHeight,
                    document.body.offsetHeight,
                    document.documentElement.offsetHeight
                );
            hsize = hsize - 72;
            $('#content').css({ 'height': hsize + 'px' });
        },

        IniBegin: function () {
            var that = this,
            controls = this.getControls();

            that.IniLoadPage();
        },


        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();
            controls.lblTitle.text("Cambio de Datos Menores");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            Session.LINEA = SessionTransac.SessionParams.DATASERVICE;
            Session.ACCESO = SessionTransac.SessionParams.USERACCESS;
            var obj = {
                strIdSession: Session.IDSESSION,
                strTelefono: Session.CLIENTE.Telephone,
                strCustomerId: Session.CLIENTE.CustomerID               
            };
            $.app.ajax({
                type: 'POST',
                cache: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/IFITransactions/ChangeMinor/GetCustomerChangeMinor',
                success: function (response) {
                   
                    var ObjCus = response.objCus;
                    var CambiarFecha = response.cambiarfecha;
                    var ObjClientDataAdd =  response.objclientDataAdd;


                    that.setSesssionsCustomer(ObjCus, CambiarFecha, ObjClientDataAdd);

                },
                complete: function () {

                    controls.txtEmail.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
                    //********** Datos del Formulario ***********/
                    controls.txtCargoDesem.val((Session.CLIENTE.Position == null) ? '' : Session.CLIENTE.Position);
                    controls.txtNameComChangeMinor.val((Session.CLIENTE.Tradename == null) ? '' : Session.CLIENTE.Tradename);
                    controls.txtPhoneChangeMinor.val((Session.CLIENTE.PhoneReference == null) ? '' : Session.CLIENTE.PhoneReference);
                    controls.txtContactocliChangeMinor.val((Session.CLIENTE.CustomerContact == null) ? '' : Session.CLIENTE.CustomerContact);
                    controls.txtMovilChangeMinor.val((Session.CLIENTE.PhoneContact == null) ? '' : Session.CLIENTE.PhoneContact);
                    controls.txtDateChangeMinor.val((Session.CLIENTE.BirthDate == null) ? '' : Session.CLIENTE.BirthDate);
                    controls.txtMovil3ChangeMinor.val((Session.CLIENTE.ContactNumberReference1 == null) ? '' : Session.CLIENTE.ContactNumberReference1);
                    controls.txtMovil4ChangeMinor.val((Session.CLIENTE.ContactNumberReference2 == null) ? '' : Session.CLIENTE.ContactNumberReference2);
                    controls.txtCalleChangeMinor.val((Session.CLIENTE.Urbanization == null) ? '' : Session.CLIENTE.Urbanization);
                    controls.txtEmail2ChangeMinor.val((Session.CLIENTE.PlaneCodeBilling == null) ? '' : Session.CLIENTE.PlaneCodeBilling);
                    controls.txtEmail3ChangeMinor.val((Session.CLIENTE.PlaneCodeInstallation == null) ? '' : Session.CLIENTE.PlaneCodeInstallation);
                    controls.txtConfirmarEmailChangeMinor.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
                    controls.txtNumberChangeMinor.val((Session.CLIENTE.Fax == null) ? '' : Session.CLIENTE.Fax);
                    controls.txtEmailChangeMinor.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
                    that.InitEstCiv(Session.CLIENTE.CivilStatusID);
                    that.InitNacionalidad(Session.CLIENTE.BirthPlaceID);
                    that.InitCacDaC();
                    that.InitPais();                    
                    
                    
                   

                    $("input[name=rdbSexo]").val([(Session.CLIENTE.Sex == null) ? '' : Session.CLIENTE.Sex]);


                    if (Session.CLIENTE.Sex == "F") {

                        controls.lblSexoCli.html("FEMENINO");
                        
                    }
                    else if (Session.CLIENTE.Sex == "M") {
                        controls.lblSexoCli.html("MASCULINO");

                    }
                    else if (Session.CLIENTE.Sex == "U") {

                        controls.lblSexoCli.html("N/A");
                    }

                    $("#txtPhoneChangeMinor").on('input', function () {
                        this.value = this.value.replace(/[^0-9]/g, '');
                    })

                    $("#txtMovilChangeMinor").on('input', function () {
                        this.value = this.value.replace(/[^0-9]/g, '');
                    });

                    $("#txtNumberChangeMinor").on('input', function () {
                        this.value = this.value.replace(/[^0-9]/g, '');
                    });

                    $("#btnGuardar").attr('disabled', true);

                    $("input,select").keypress(function (e) {
                        $("#btnGuardar").attr('disabled', false);
                    });

                    $("select").change(function (e) {
                        $("#btnGuardar").attr('disabled', false);
                    });

                    $("textarea").keypress(function (e) {
                        $("#btnGuardar").attr('disabled', false);
                    });

                    $("#txtNameComChangeMinor").keypress(function (e) {
                        var regex = new RegExp(/^[a-zA-Z\s]+$/);
                        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                        if (regex.test(str)) {
                            return true;
                        }
                        else {
                            e.preventDefault();
                            return false;
                        }
                    });

                    $("#txtContactocliChangeMinor").keypress(function (e) {
                        var regex = new RegExp(/^[a-zA-Z\s]+$/);
                        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                        if (regex.test(str)) {
                            return true;
                        }
                        else {
                            e.preventDefault();
                            return false;
                        }
                    });

                    $("#txtCargoDesem").keypress(function (e) {
                        var regex = new RegExp(/^[a-zA-Z\s]+$/);
                        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                        if (regex.test(str)) {
                            return true;
                        }
                        else {
                            e.preventDefault();
                            return false;
                        }
                    });
                }

            });

            
            
            //********** Datos del Cliente ***********/
            controls.lblContrato.html((Session.CLIENTE.ContractID == null) ? '' : Session.CLIENTE.ContractID);
            controls.lblCustomerID.html((Session.CLIENTE.CustomerID == null) ? '' : Session.CLIENTE.CustomerID);
            controls.lblTipoCliente.html((Session.CLIENTE.CustomerType == null) ? '' : Session.CLIENTE.CustomerType);
            controls.lblCliente.html((Session.CLIENTE.BusinessName == null) ? '' : Session.CLIENTE.BusinessName);


            controls.lblRepren_Legal.html((Session.CLIENTE.LegalAgent == null) ? '' : Session.CLIENTE.LegalAgent);
            controls.lblPlan.html((Session.LINEA.Plan == null) ? '' : Session.LINEA.Plan);
            controls.lblFechaActivacion.html((Session.CLIENTE.FechaActivacion == null) ? '' : Session.CLIENTE.FechaActivacion);
            controls.lblCicloFact.html((Session.CLIENTE.BillingCycle == null) ? '' : Session.CLIENTE.BillingCycle);
            controls.lblLimiteCred.html((Session.CLIENTE.objPostDataAccount.CreditLimit == null) ? '' : 'S/ ' + Session.CLIENTE.objPostDataAccount.CreditLimit);


            controls.lblNroTelefono.html((Session.CLIENTE.Telephone == null) ? '' : Session.CLIENTE.Telephone);
            controls.lblContacto.html((Session.CLIENTE.FullName == null) ? '' : Session.CLIENTE.FullName);
            controls.lblCargDesemp.html((Session.CLIENTE.Position == null) ? '' : Session.CLIENTE.Position);
            controls.lblDNI_RUC.html((Session.CLIENTE.DNIRUC == null) ? '' : Session.CLIENTE.DNIRUC);
            controls.lblTelefono.html((Session.CLIENTE.PhoneReference == null) ? '' : Session.CLIENTE.PhoneReference);
            controls.lblCelular.html((Session.CLIENTE.PhoneContact == null) ? '' : Session.CLIENTE.PhoneContact);
            controls.lblFax.html((Session.CLIENTE.Fax == null) ? '' : Session.CLIENTE.Fax);
            controls.lblEmail.html((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
            controls.lblNomComercial.html((Session.CLIENTE.Tradename == null) ? '' : Session.CLIENTE.Tradename);
            controls.lblContactoCli.html((Session.CLIENTE.CustomerContact == null) ? '' : Session.CLIENTE.CustomerContact);
            controls.lblFechaNac.html((Session.CLIENTE.BirthDate == null) ? '' : Session.CLIENTE.BirthDate);
            controls.lblNacionalidad.html((Session.CLIENTE.BirthPlace == null) ? '' : Session.CLIENTE.BirthPlace);
            if (Session.CLIENTE.Sex == "F") {

                controls.lblSexoCli.text("FEMENINO");
            }
            else if (Session.CLIENTE.Sex == "M") {
                controls.lblSexoCli.text("MASCULINO");
            }
            else if (Session.CLIENTE.Sex == "U") {

                controls.lblSexoCli.html("N/A");
            }

            controls.lblEstadoCiv.html((Session.CLIENTE.CivilStatus == null) ? '' : Session.CLIENTE.CivilStatus);

            //********** Direccíón de Facturación ***********/
            controls.lblDireccion.html((Session.CLIENTE.Direccion_Despacho == null) ? '' : Session.CLIENTE.Direccion_Despacho);
            controls.lblNotasDirec.html((Session.CLIENTE.LegalUrbanization == null) ? '' : Session.CLIENTE.LegalUrbanization);
            controls.lblPais.html((Session.CLIENTE.Country == null) ? '' : Session.CLIENTE.Country);
            controls.lblDepartamento.html((Session.CLIENTE.Departament_Fact == null) ? '' : Session.CLIENTE.Departament_Fact);
            controls.lblProvincia.html((Session.CLIENTE.provincia == null) ? '' : Session.CLIENTE.provincia);
            controls.lblDistrito.html((Session.CLIENTE.District == null) ? '' : Session.CLIENTE.District);
            controls.lblCodUbigeo.html((Session.CLIENTE.InstallUbigeo == null) ? '' : Session.CLIENTE.InstallUbigeo);
            controls.txtEmail.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
            //********** Datos del Formulario ***********/
            controls.txtCargoDesem.val((Session.CLIENTE.Position == null) ? '' : Session.CLIENTE.Position);
            controls.txtNameComChangeMinor.val((Session.CLIENTE.Tradename == null) ? '' : Session.CLIENTE.Tradename);
            controls.txtPhoneChangeMinor.val((Session.CLIENTE.PhoneReference == null) ? '' : Session.CLIENTE.PhoneReference);
            controls.txtContactocliChangeMinor.val((Session.CLIENTE.CustomerContact == null) ? '' : Session.CLIENTE.CustomerContact);
            controls.txtMovilChangeMinor.val((Session.CLIENTE.PhoneContact == null) ? '' : Session.CLIENTE.PhoneContact);
            controls.txtDateChangeMinor.val((Session.CLIENTE.BirthDate == null) ? '' : Session.CLIENTE.BirthDate);
            controls.txtNumberChangeMinor.val((Session.CLIENTE.Fax == null) ? '' : Session.CLIENTE.Fax);
            controls.txtEmailChangeMinor.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
            controls.txtConfirmarEmailChangeMinor.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);
          

            $("input[name=rdbSexo]").val([(Session.CLIENTE.Sex == null) ? '' : Session.CLIENTE.Sex]);
           
            if (Session.CLIENTE.Sex == "F") {
                
                controls.lblSexoCli.html("FEMENINO");

            }
            else if (Session.CLIENTE.Sex == "M") {
                controls.lblSexoCli.html("MASCULINO");

            }
            else if (Session.CLIENTE.Sex == "U") {

                controls.lblSexoCli.html("N/A");
            }



            $("#txtPhoneChangeMinor").on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });

            $("#txtMovilChangeMinor").on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });

            $("#txtNumberChangeMinor").on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });

            $("#txtNameComChangeMinor").keypress(function (e) {
                var regex = new RegExp(/^[a-zA-Z\s]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $("#txtContactocliChangeMinor").keypress(function (e) {
                var regex = new RegExp(/^[a-zA-Z\s]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $("#txtCargoDesem").keypress(function (e) {
                var regex = new RegExp(/^[a-zA-Z\s]+$/);
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $("#txtMovil3ChangeMinor").on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });

            $("#txtMovil4ChangeMinor").on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });




        },
        resp: "",
        LoadPag: function () {
            var that = this, controls = this.getControls();
            $.blockUI({
                message: controls.myModalLoad,
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000000',
                    '-webkit-border-radius': '50px',
                    '-moz-border-radius': '50px',
                    opacity: .7,
                    color: '#fff'
                }
            });

        },

        chkEmail_Change: function (sender, arg) {
            var that = this,
                control = that.getControls(),
                chkEmail = control.chkEmail;

            if (chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }
        },




        maximizarWindow: function () {
            top.window.moveTo(0, 0);
            if (document.all) {
                top.window.resizeTo(screen.availWidth, screen.availHeight);
            } else if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;

                }
            }
        },



        btnConstancia_click: function () {
            var that = this;


            var PDFRoute = that.RutaArchivo;
            var IdSession = Session.IDSESSION;
            if (PDFRoute != "") {
                ReadRecordSharedFile(IdSession, PDFRoute);
            }
        },

        btnGuardar_click: function () {
            if ($('#frmChangeMinor').valid()) {
                var that = this,
                    controls = this.getControls();

                that.isPostBackFlag = that.NumeracionUNO;
             
                if (!that.ValidateControl()) {
                    return false;
                }
                if (!that.validarMaxCaracteresNote()) {
                    return false;
                }

                confirm("¿Seguro que desea guardar la transacción?", 'Confirmar', function () {
                    that.Loading();
                    that.SaveTransactionCambioDatosM();

                }, function () {
                    $("#hidAccion").val("R");
                    return false;
                });
            }
        },


        Loading: function () {
            var that = this;
            var controls = that.getControls();


            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
            });
        },

        validarMaxCaracteresNote: function () {

            nombre2 = $('#txtNote').val().length;
            if (nombre2 < 3801) {
                return true;
            }
            else {
                alert('El maximo de caracteres permitidos en el campo notas es de 3800');
                return false;
            }
        },

        ValidateControl: function () {
            var that = this,
                control = that.getControls();

            if ($("#chkEmail").prop("checked")) {
                if ($("#txtEmail").val() == "") {
                    alert("Ingresar email", 'Alerta', function () {
                        control.txtEmail.focus();
                    }); return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test($("#txtEmail").val());
                if (!blvalidar) {
                    alert("Ingresar email válido", 'Alerta', function () {
                        control.txtEmail.select();
                    }); return false;
                }
            }

            if (validarFecha('txtDateChangeMinor') == false) {
                document.frmMain.txtDateChangeMinor.focus();
                return false;
            }

            if (control.cboCACDAC.val() == "-1" || control.cboCACDAC.val() == "") {
                alert("Seleccione Punto de Atención.", "Alerta");
                return false;
            }

            if (control.cbocivilstatus.val() == "-1" || control.cbocivilstatus.val() == "") {
                alert("Porfavor Seleccione un Estado Civil.", "Alerta");
                return false;
            }

            if (control.cboNacionalidadChangeMinor.val() == "-1" || control.cboNacionalidadChangeMinor.val() == "") {
                alert("Seleccione la nacionalidad del cliente.", "Alerta");
                return false;
            }



            return true;
        },


        btnCerrar_Click: function () {
            parent.window.close();
        },


        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },



        BlockControl: function () {
            var that = this,
            control = that.getControls();


            control.txtCargoDesem.prop('disabled', true);
            control.txtPhoneChangeMinor.prop('disabled', true);
            control.txtMovilChangeMinor.prop('disabled', true);
            control.txtNumberChangeMinor.prop('disabled', true);
            control.txtEmailChangeMinor.prop('disabled', true);
            control.txtNameComChangeMinor.prop('disabled', true);
            control.txtContactocliChangeMinor.prop('disabled', true);
            control.txtDateChangeMinor.prop('disabled', true);
            control.cboNacionalidadChangeMinor.prop('disabled', true);
            control.rdbHombre.prop('disabled', true);
            control.rdbMujer.prop('disabled', true);
            control.rdbNA.prop('disabled', true);
            control.cbocivilstatus.prop('disabled', true);
            control.cboCACDAC.prop('disabled', true);
            control.txtNote.prop('disabled', true);
            control.cboPaisMod.prop('disabled', true);
            control.txtMovil3ChangeMinor.prop('disabled', true);
            control.txtMovil4ChangeMinor.prop('disabled', true);
            control.cboDepMod.prop('disabled', true);
            control.cboProvinciaMod.prop('disabled', true);
            control.cboDistritoMod.prop('disabled', true);
            control.txtEmail2ChangeMinor.prop('disabled', true);
            control.txtEmail3ChangeMinor.prop('disabled', true);
            control.txtEmail.prop('disabled', true);
            control.chkEmail.prop('disabled', true);
            control.txtConfirmarEmailChangeMinor.prop('disabled', true);
            control.txtCalleChangeMinor.prop('disabled', true);

        },


        SaveTransactionCambioDatosM: function () {

          
            var that = this,
                controls = that.getControls();

            that.modelChangeMinor.strIdSession = Session.IDSESSION;
            that.modelChangeMinor.strCargo = $('#txtCargoDesem').val();
            that.modelChangeMinor.strPhone = $('#txtPhoneChangeMinor').val();
            that.modelChangeMinor.strMovil = $('#txtMovilChangeMinor').val();
            that.modelChangeMinor.strFax = $('#txtNumberChangeMinor').val();
            that.modelChangeMinor.strMailChange = $('#txtEmailChangeMinor').val();
            that.modelChangeMinor.strNombreComercial = $('#txtNameComChangeMinor').val();
            that.modelChangeMinor.strContactoCliente = $('#txtContactocliChangeMinor').val();
            that.modelChangeMinor.dateFechaNacimiento = $('#txtDateChangeMinor').val();
            that.modelChangeMinor.strNacionalidad = $('#cboNacionalidadChangeMinor option:selected').text();
            that.modelChangeMinor.strNacionalidadId = $('#cboNacionalidadChangeMinor option:selected').val();
            that.modelChangeMinor.strSexo = $('input[name=rdbSexo]:checked').val();
            that.modelChangeMinor.strEstadoCivil = $('#cbocivilstatus option:selected').text();
            that.modelChangeMinor.strEstadoCivilId = $('#cbocivilstatus option:selected').val();
            that.modelChangeMinor.strCacDac = $('#cboCACDAC option:selected').text();
            that.modelChangeMinor.strCacDacId = $('#cboCACDAC option:selected').val();
            that.modelChangeMinor.strNote = $("#txtNote").val();
            that.modelChangeMinor.strCustomerId = Session.CLIENTE.CustomerID;
            that.modelChangeMinor.strObjidContacto = Session.CLIENTE.IdContactObject;
            that.modelChangeMinor.strNombres = Session.CLIENTE.Name;
            that.modelChangeMinor.strApellidos = Session.CLIENTE.LastName;
            that.modelChangeMinor.strTelefono = Session.CLIENTE.Telephone;
            that.modelChangeMinor.RepresentLegal = Session.CLIENTE.LegalAgent;
            that.modelChangeMinor.strNameComplet = Session.CLIENTE.FullName;
            that.modelChangeMinor.strTipoocumento = Session.CLIENTE.DocumentType;
            that.modelChangeMinor.AdressDespatch = Session.CLIENTE.Address;
            that.modelChangeMinor.Accion = "R";
            that.modelChangeMinor.DNI_RUC = Session.CLIENTE.DocumentNumber;
            that.modelChangeMinor.fullNameUser = Session.ACCESO.fullName,
            that.modelChangeMinor.chkEmail = controls.chkEmail.is(':checked');
            // nuevos campos Adicionales
            that.modelChangeMinor.strPais = $('#cboPaisMod option:selected').text();
            that.modelChangeMinor.StrPHONE1 = $("#txtMovil3ChangeMinor").val();
            that.modelChangeMinor.StrPHONE2 = $("#txtMovil4ChangeMinor").val();
            that.modelChangeMinor.StrADDRESS = $("#txtCalleChangeMinor").val();
            that.modelChangeMinor.StrCOUNTRY_DES = $('#cboPaisMod option:selected').text();
            that.modelChangeMinor.StrCOUNTRY_ID = $('#cboPaisMod option:selected').val();
            that.modelChangeMinor.StrDEPARTAMENTO = $('#cboDepMod option:selected').text();
            that.modelChangeMinor.StrDEPARTAMENTO_ID = $('#cboDepMod option:selected').val();
            that.modelChangeMinor.StrPROVINCIA = $('#cboProvinciaMod option:selected').text();
            that.modelChangeMinor.StrPROVINCIA_ID = $('#cboProvinciaMod option:selected').val();
            that.modelChangeMinor.StrDISTRITO = $('#cboDistritoMod option:selected').text();
            that.modelChangeMinor.StrDISTRITO_ID = $('#cboDistritoMod option:selected').val();
            that.modelChangeMinor.StrEMAIL1 = $("#txtEmail2ChangeMinor").val();
            that.modelChangeMinor.StrEMAIL2 = $("#txtEmail3ChangeMinor").val();
            that.modelChangeMinor.StrEmailEnviar = $("#txtEmail").val();


            if (controls.chkEmail[0].checked) {
                that.modelChangeMinor.Flag_Email = 'true';
            } else {
                that.modelChangeMinor.Flag_Email = 'false';
            }
            //datos adicionales

            that.modelChangeMinor.account = Session.CLIENTE.Account;
            that.modelChangeMinor.contractId = Session.CLIENTE.ContractID;
            that.modelChangeMinor.currentUser = Session.ACCESO.login;
            that.modelChangeMinor.customerId = Session.CLIENTE.CustomerID;
            that.modelChangeMinor.tipo = TYPIFICATION.Tipo;
            that.modelChangeMinor.claseDes = TYPIFICATION.ClaseDes;
            that.modelChangeMinor.subClaseDes = TYPIFICATION.SubClaseDes;
            that.modelChangeMinor.claseCode = TYPIFICATION.ClaseId;
            that.modelChangeMinor.subClaseCode = TYPIFICATION.SubClaseId;
            that.modelChangeMinor.tipoCode = TYPIFICATION.TipoId;

            //fin--

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(that.modelChangeMinor),
                url: '/IFITransactions/ChangeMinor/SaveTransactionCambioDatosM',
                error: function (data) {
                    alert(that.strMensajeDeError, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);
                },
                success: function (response) {
                    that.MensajeEmail = response.MensajeEmail;
                    that.vDesInteraction = response.vDesInteraction;
                    that.InteractionId = response.vInteractionId;
                    that.RutaArchivo = response.strRutaArchivo;
                    that.hidCasoId = response.vInteractionId;
                    
                    if (that.InteractionId != '') {
                        $("#btnConstancia").attr('disabled', false); //Activa                        
                        alert(that.vDesInteraction, "Informativo");  //that.vDesInteraction

                        that.BlockControl();
                        $("#btnGuardar").attr('disabled', true);
                    }
                    else if (that.InteractionId=="") {
                        $("#btnConstancia").attr('disabled', true);
                        $("#btnGuardar").attr('disabled', true);
                        alert(that.vDesInteraction, "Informativo");  
                    }
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        },


        setSesssionsCustomer: function (objCus, cambiarfecha, objclientDataAdd) {


            Session.CLIENTE.Position = objCus.CARGO;
            Session.CLIENTE.Tradename = objCus.NOMBRE_COMERCIAL;
            Session.CLIENTE.PhoneReference = objCus.TELEF_REFERENCIA;
            Session.CLIENTE.CustomerContact = objCus.CONTACTO_CLIENTE;
            Session.CLIENTE.PhoneContact = objCus.TELEFONO_CONTACTO;
            Session.CLIENTE.Fax = objCus.FAX;
            Session.CLIENTE.BirthDate = cambiarfecha;
            Session.CLIENTE.Email = objCus.EMAIL;
            Session.CLIENTE.Sex = objCus.SEXO;
            Session.CLIENTE.ContactNumberReference1 = objclientDataAdd.PHONE1;
            Session.CLIENTE.ContactNumberReference2 = objclientDataAdd.PHONE2;
            Session.CLIENTE.Urbanization = objclientDataAdd.ADDRESS;
            Session.CLIENTE.PlaneCodeBilling = objclientDataAdd.EMAIL1;            
            Session.CLIENTE.PlaneCodeInstallation = objclientDataAdd.EMAIL2;           
            Session.CLIENTE.CivilStatusID = objCus.ESTADO_CIVIL_ID;
            Session.CLIENTE.BirthPlaceID = objCus.LUGAR_NACIMIENTO_ID;
            Session.CLIENTE.Departament = objclientDataAdd.DEPARTAMENTO;
            Session.CLIENTE.Province = objclientDataAdd.PROVINCIA;
            Session.CLIENTE.District = objclientDataAdd.DISTRITO;
          


        },
        InitCacDaC: function () {

            var that = this,
                controls = that.getControls(),
                objCacDacType = {},
                parameters = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var cacdac = results.Cac;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        cache: false,
                        data: JSON.stringify(objCacDacType),
                        url: '/IFITransactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.cboCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));

                                    itemSelect = value.Code;

                                } else {
                                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },

        InitEstCiv: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objEstCivType = {},
                parameters = {};

            objEstCivType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var EstCiv = results.MAS_DES;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objEstCivType),
                        url: '/IFITransactions/CommonServices/GetEstCivType',
                        success: function (response) {
                            controls.cbocivilstatus.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.EstCivTypes, function (index, value) {

                                if (EstCiv === value.Description) {
                                    controls.cbocivilstatus.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cbocivilstatus.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cbocivilstatus option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cbocivilstatus option[value=" + pid + "]").attr("selected", true); }
                        }
                    });
                }
            });
        },

        InitNacionalidad: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objNacType = {},
                parameters = {};

            objNacType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Nac = results.NACIONALIDAD;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objNacType),
                        url: '/IFITransactions/CommonServices/GetNacType',
                        success: function (response) {
                            controls.cboPaisMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.NacTypes, function (index, value) {

                                if (Nac === value.Description) {
                                    controls.cboNacionalidadChangeMinor.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboNacionalidadChangeMinor.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboNacionalidadChangeMinor option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboNacionalidadChangeMinor option[value=" + pid + "]").attr("selected", true); }
                        }
                    });
                }
            });
        },

        InitPais: function () {
            var that = this,
                controls = that.getControls(),
                objNacType = {},
                parameters = {};

            objNacType.strIdSession = Session.IDSESSION;

            var obj = {
                strIdSession: Session.IDSESSION,
                strTelefono: Session.CLIENTE.Telephone,
                strCustomerId: Session.CLIENTE.CustomerID
            };
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(obj),
                url: '/IFITransactions/ChangeMinor/GetCustomerChangeMinor',
                success: function (results) {
                    var CodPais = results.objclientDataAdd.COUNTRY_ID;
                   
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objNacType),
                        url: '/IFITransactions/CommonServices/GetNacType',
                        success: function (response) {

                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.NacTypes, function (index, value) {

                                if (CodPais == value.Code) {
                                    controls.cboPaisMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;


                                } else {
                                    controls.cboPaisMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboPaisMod option[value=" + itemSelect + "]").attr("selected", true);
                            }                                                                             
                                                       

                            if ($('#cboPaisMod option:selected').text() == "PERU" || $('#cboPaisMod option:selected').text() == "Peru" || $('#cboPaisMod option:selected').text() == "Perú" || $('#cboPaisMod option:selected').text() == "PERÚ") {

                                $("#cboPaisMod").attr('disabled', true);
                            }
                            else {

                                $("#cboPaisMod").attr('enabled', true);

                                $("#cboDepMod").attr('disabled', true);

                                $("#cboProvinciaMod").attr('disabled', true);

                                $("#cboDistritoMod").attr('disabled', true);
                            }

                            that.InitDepartamentosSesion();

                        }
                    });
                }
            });

        },

        InitDepartamentos: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos1Type = {},
                parameters = {};

            objUbigeos1Type.strIdSession = Session.IDSESSION;
            objUbigeos1Type.dep = 0;
            objUbigeos1Type.prov = 0;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Ubigeo1 = results.DESCRIPCION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos1Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboDepMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (Ubigeo1 === value.Description) {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboDepMod option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cboDepMod option[value=" + pid + "]").attr("selected", true); }
                            controls.cboProvinciaMod.prop('disabled', true);
                            controls.cboDistritoMod.prop('disabled', true);
                        }
                    });
                }
            });
        },

        InitDepartamentosSesion: function (pid) {

            
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos1Type = {},
                parameters = {};

            objUbigeos1Type.strIdSession = Session.IDSESSION;
            objUbigeos1Type.dep = 0;
            objUbigeos1Type.prov = 0;

            var obj = {
                strIdSession: Session.IDSESSION,
                strTelefono: Session.CLIENTE.Telephone,
                strCustomerId: Session.CLIENTE.CustomerID
            };
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(obj),
                url: '/IFITransactions/ChangeMinor/GetCustomerChangeMinor',
                success: function (results) {
                    var coddep = results.objclientDataAdd.DEPARTAMENTO;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos1Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            
                            controls.cboDepMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (coddep === value.Code) {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboDepMod option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if ($('#cboPaisMod option:selected').text() != 'PERU') {
                                $("#cboDepMod").prop("disabled", true);
                            } else {
                                $("#cboDepMod option:contains(" + coddep + ")").attr('selected', true);
                                controls.cboProvinciaMod.prop('disabled', true);
                                controls.cboDistritoMod.prop('disabled', true);
                            }
                            that.InitProvinciasSesion();
                            controls.cboProvinciaMod.prop('disabled', true);
                            controls.cboDistritoMod.prop('disabled', true);
                        }
                    });
                }
            });
        },

        InitProvincias: function (pid) {
         
            if (pid == null || pid.toString == "undefined") { pid = ""; }
          
            var that = this,
                controls = that.getControls(),
                objUbigeos2Type = {},
                parameters = {};

            objUbigeos2Type.strIdSession = Session.IDSESSION;

            objUbigeos2Type.dep = controls.cboDepMod.val();
            if (objUbigeos2Type.dep == '') {
                controls.cboProvinciaMod.empty();
                controls.cboDistritoMod.empty();
                controls.cboProvinciaMod.prop('disabled', true);
                controls.cboDistritoMod.prop('disabled', true);
            } else {
            objUbigeos2Type.prov = 0;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;
                                       
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Ubigeo2 = results.DESCRIPCION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos2Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboProvinciaMod.empty();                           
                            controls.cboProvinciaMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            controls.cboDistritoMod.empty();

                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (Ubigeo2 === value.Description) {
                                    controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboProvinciaMod option[value=" + itemSelect + "]").attr("selected", true);
                                    }
                           
                           
                            controls.cboProvinciaMod.prop('disabled', false);

                                }
                            });

                            controls.cboDistritoMod.prop('disabled', true);
                        }
                    });
            controls.cboProvinciaMod.prop('disabled', true);
                controls.cboProvinciaMod.append($('<option>', { value: '', html: 'Seleccionar' }));
            that.getRulesUbigeoProv();
            controls.cboDistritoMod.prop('disabled', true);
            }
        },

        InitProvinciasSesion: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos2Type = {};
              

            objUbigeos2Type.strIdSession = Session.IDSESSION;
            objUbigeos2Type.dep = controls.cboDepMod.val();
            objUbigeos2Type.prov = 0;

            var obj = {
                strIdSession: Session.IDSESSION,
                strTelefono: Session.CLIENTE.Telephone,
                strCustomerId: Session.CLIENTE.CustomerID
            };

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(obj),
                url: '/IFITransactions/ChangeMinor/GetCustomerChangeMinor',
                success: function (results) {
                    var codProv = results.objclientDataAdd.PROVINCIA;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos2Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboProvinciaMod.empty();                         
                            controls.cboProvinciaMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            controls.cboDistritoMod.empty();
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (codProv === value.Code) {
                                    controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;                                

                                } else {
                                    controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboProvinciaMod option[value=" + itemSelect + "]").attr("selected", true);
                            }
                            if ($('#cboPaisMod option:selected').text() != 'PERU') {
                                $("#cboProvinciaMod").prop("disabled", true);
                            } else {
                                $("#cboProvinciaMod").prop("disabled", false);
                                $("#cboProvinciaMod option:contains(" + codProv + ")").attr('selected', true);
                            }

                           
                            that.InitDistritosSesion();
                            controls.cboDistritoMod.prop('disabled', true);
                                     }
                    });
                                }
            });
            that.getRulesUbigeoProv();
            controls.cboDistritoMod.prop('disabled', true);
        },

        InitDistritos: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }
           
            var that = this,
                controls = that.getControls(),
                objUbigeos3Type = {},
                parameters = {};

          

            if ($('#cboProvinciaMod option:selected').val() != "") {
                
                objUbigeos3Type.strIdSession = Session.IDSESSION;
                objUbigeos3Type.dep = controls.cboDepMod.val();
                objUbigeos3Type.prov = controls.cboProvinciaMod.val();

                                    }
            else
            {
                controls.cboDistritoMod.empty();
                $('#cboDistritoMod').prop('disabled', true);
            }

           

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/IFITransactions/CommonServices/GetUsers',
                success: function (results) {
                    var Ubigeo3 = results.DESCRIPCION;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos3Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboDistritoMod.empty();
                            controls.cboDistritoMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (Ubigeo3 === value.Description) {
                                    controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboDistritoMod option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            controls.cboDistritoMod.prop('disabled', false);
                        }
                    });
                }
            });

            that.getRulesUbigeoDist();
           
        },

        InitDistritosSesion: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos3Type = {},
                parameters = {};

            objUbigeos3Type.strIdSession = Session.IDSESSION;
            objUbigeos3Type.dep = controls.cboDepMod.val();
            objUbigeos3Type.prov = controls.cboProvinciaMod.val();

            var obj = {
                strIdSession: Session.IDSESSION,
                strTelefono: Session.CLIENTE.Telephone,
                strCustomerId: Session.CLIENTE.CustomerID
            };
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(obj),
                url: '/IFITransactions/ChangeMinor/GetCustomerChangeMinor',
                success: function (results) {
                    var codDist = results.objclientDataAdd.DISTRITO;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objUbigeos3Type),
                        url: '/IFITransactions/CommonServices/GetUbigeoType',
                        success: function (response) {
                            controls.cboDistritoMod.empty();
                            controls.cboDistritoMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.UbigeoTypes, function (index, value) {

                                if (codDist === value.Code) {
                                    controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                        $("#cboDistritoMod option[value=" + itemSelect + "]").attr("selected", true);
                                    }

                            if ($('#cboPaisMod option:selected').text() != 'PERU') {
                                $("#cboDistritoMod").prop("disabled", true);
                            } else {
                                $("#cboDistritoMod").prop("disabled", false);
                                $("#cboDistritoMod option:contains(" + codDist + ")").attr('selected', true);
                                }
                        }
                    });
                }
            });
            that.getRulesUbigeoDist();
        },



        getRulesUbigeoProv: function () {
         
            $('#frmChangeMinor').validate({ // initialize the plugin
                rules: {
                    nmcboProvinciaMod: {
                        required: true
                    }
                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {
                    nmcboProvinciaMod: {
                        required: "* La opcion es Requerida",
                    }
                }

            });
        },

        getRulesUbigeoDist: function () {
            $('#frmChangeMinor').validate({ // initialize the plugin
                rules: {
                    nmcboDistritoMod: {
                        required: true
                    }
                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {
                    nmcboDistritoMod: {
                        required: "* La opcion es Requerida",
                    }
                }

            });
        },


        getBusinessRules: function (SubClaseCode) {
            var that = this, controls = that.getControls();
            controls.divReglas.empty().html('');
            $.app.ajax({
                type: "POST",
                url: "/IFITransactions/CommonServices/GetBusinessRules",
                data: {
                    strIdSession: Session.IDSESSION,
                    strSubClase: SubClaseCode
                },
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divReglas.html(list[0].REGLA);
                        }
                    }

                }
            });
        },

        ShowConstancy: function (filepath, Filename) {
            var that = this,
               controls = that.getControls();
            var url = that.strUrl + '/GenerateRecord/ExistFile';

            $.app.ajax({
                type: 'GET',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: url,
                data: { strFilePath: filepath, strFileName: Filename, strIdSession: Session.IDSESSION },
                success: function (result) {
                    if (result.Exist == false) {
                        alert('No existe el Archivo.', "Alerta");
                    } else {
                        var url = that.strUrl + '/GenerateRecord/showInvoice';
                        window.open(url + "?strFilePath=" + Session.filepath + "&strFileName=" + Session.Filename + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION, "FACTURA ELECTRÓNICA", "");
                    }

                },
                error: function (ex) {
                    alert('No existe el Archivo.', "Alerta");
                }
            });
        },

        Round: function (cantidad, decimales) {

            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },


        pad: function (s) { return (s < 10) ? '0' + s : s; },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        loadPage: function () {
            $.blockUI({
                message: '<div align="center"><img src="' + this.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
            });
        },


    };


    $.fn.INTChangeMinor = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTChangeMinor'),
                options = $.extend({}, $.fn.INTChangeMinor.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTChangeMinor', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }

        });
        return value || this;
    };

    $.fn.INTChangeMinor.defaults = {
    }

    $('#divBody').INTChangeMinor();
})(jQuery);