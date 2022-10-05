var HiddenPageAuth = {};

function ValidateAccess(pag, controls, action, opcion, pagina, param, product) {
    HiddenPageAuth.hidOpcion = action;
    var strUrlModal = location.protocol + '//' + location.host + '/IFITransactions/Auth/AuthUserHtml';
    confirmAlert('Se requiere autorización del Jefe/Supervisor.', 'Confirmar', function () {
        $.window.open({
            modal: true,
            type: 'post',
            title: "SIACUNICO - Autenticación",
            url: strUrlModal,
            data: {},
            width: 360,
            height: 310,
            buttons: {
                Validar: {
                    id: 'btnSigInAuth',
                    click: function () {
                        if (AuthenticationUser(pag, controls, product)) {
                            this.close();
                        };
                    }
                },
                Cancelar: {
                    id: 'btnCancelAuth',
                    click: function (sender, args) {
                        f_cancelAuth();
                        this.close();
                    }
                }
            },
            complete: function () {
                var strUrlController = '/IFITransactions/CommonServices/UserValidate_PageLoad';       

                $.ajax({
                    type: 'POST',
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'JSON',
                    url: strUrlController,
                    data: JSON.stringify(param),
                    success: function (response) {
                        HiddenPageAuth.hidPagina_Validar = response.hidPagina;

                        HiddenPageAuth.hidPagDCM_Validar = response.hidPagDCM;

                        HiddenPageAuth.hidMonto_Validar = response.hidMonto;

                        HiddenPageAuth.hidUnidad_Validar = response.hidUnidad;

                        HiddenPageAuth.hidOpcion_Validar = response.hidOpcion;

                        HiddenPageAuth.hidModalidad_Validar = response.hidModalidad;

                        HiddenPageAuth.hidLogin_Validar = response.hidLogin;

                        HiddenPageAuth.hidCO_Validar = response.hidCO;

                        HiddenPageAuth.hidMigracion_Validar = response.hidMigracion;

                        HiddenPageAuth.hidDescripcionProceso_Validar = response.hidDescripcionProceso;

                        HiddenPageAuth.hidConcepto_Validar = response.hidConcepto;

                        HiddenPageAuth.hidAccionDetEnt_Validar = response.hidAccionDetEnt;

                        HiddenPageAuth.lblTitulo_Validar = response.lblTitulo;

                        HiddenPageAuth.hidAccion_Validar = response.hidAccion;

                        HiddenPageAuth.hidTelefono_Validar = response.hidTelefono;

                        HiddenPageAuth.hidMotivoA_Validar = response.hidMotivoA;

                        HiddenPageAuth.hidTipoA_Validar = response.hidTipoA;

                        if (response.ReseteoLinea) {
                            alert("Reseteo de Linea.", "Informativo");
                            return;
                        }
                    },
                    error: function (error) {
                        alert('Error: ' + error + ".", "Alerta");
                    }
                });
            }
        });
        return;
    }, function () { f_cancelAuth(); return; });
}

function AuthenticationUser(pag, controls, product) {
    var user = $("#txtUsernameAuth").val();
    var password = $("#txtPasswordAuth").val();
    if (user === "") {
        alert('Debe ingresar un nombre de usuario.', "Alerta");
        return false;
    }

    if (password === "") {
        alert('Debe ingresar una contraseña.', "Alerta");
        return false;
    }

    var transaccion = "";
    var strUrlController = '/IFITransactions/CommonServices/ValidateUser';

    var strUrlLogo = window.location.protocol + '//' + window.location.host + '/Images/loading2.gif';

    var param = {
        "strIdSession": "1234567",
        'transaction': '018462319841578347854',
        'txtUsuario': user,
        'txtPass': password,
        'hidPagina': HiddenPageAuth.hidPagina_Validar, //$('#hidPagina_Validar').val(),
        'hidMonto': HiddenPageAuth.hidMonto_Validar, //$('#hidMonto_Validar').val(),
        'hidUnidad': HiddenPageAuth.hidUnidad_Validar, //$('#hidUnidad_Validar').val(),
        'hidModalidad': HiddenPageAuth.hidModalidad_Validar, //$('#hidModalidad_Validar').val(),
        'hidDescripcionProceso': HiddenPageAuth.hidDescripcionProceso_Validar, //$('#hidDescripcionProceso_Validar').val(),
        'hidTipoA': HiddenPageAuth.hidTipoA_Validar, //$('#hidTipoA_Validar').val(),
        'hidCo': HiddenPageAuth.hidCO_Validar, //$('#hidCO_Validar').val(),
        'hidMotivoA': HiddenPageAuth.hidMotivoA_Validar, //$('#hidMotivoA_Validar').val(),
        'hidTelefono': HiddenPageAuth.hidTelefono_Validar, //$('#hidTelefono_Validar').val(),
        'hidAccion': HiddenPageAuth.hidAccion_Validar, //$('#hidAccion_Validar').val(),
        'hidVeces': HiddenPageAuth.hidVeces_Validar, //$('#hidVeces_Validar').val(),
        'hidOpcion': HiddenPageAuth.hidOpcion_Validar, //$('#hidOpcion_Validar').val(),
        'hidPagDCM': HiddenPageAuth.hidPagDCM_Validar, //$('#hidPagDCM_Validar').val(),
        'hidConcepto': HiddenPageAuth.hidConcepto_Validar, //$('#hidConcepto_Validar').val(),
        'transaccion': transaccion,
        'tecnologia': product
    };

    $.blockUI({
        message: '<div align="center"><img src="' + strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
        css:
        {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });

    $.ajax({
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlController,
        data: JSON.stringify(param),
        success: function (response) {
            CloseValidation(response, pag, controls);
        },
        error: function (xhr, status, error) {
            alert('Ocurrió un error realizando la operación.', "Error");
            f_cancelAuth(); return;
        }
    });
    return true;
}