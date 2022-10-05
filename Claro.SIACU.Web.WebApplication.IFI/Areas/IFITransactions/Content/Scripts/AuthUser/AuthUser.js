var xthat = this;
function ValidateUserIFI(option, fn_success, fn_failled, fn_cancel, fn_error) {
   // var xthat = this;
    console.log(111);
    $.window.open({
        autoSize: true,
        url: "/IFITransactions/Auth/AuthUserHtml",
        type: 'POST',
        title: 'Autorización',
        modal: false,
        width: 931,
        height: 400,
        buttons: {
            Aceptar: {
                class: 'btn-primary',
                click: function (sender, args) {
                    var usu = $('#txtUsernameAuth').val();
                    var pass = $('#txtPasswordAuth').val();
                    var $this = this;
                    $.ajax({
                        type: "POST",
                        cache: false,
                        dataType: "json",
                        url: '/IFITransactions/CommonServices/CheckingUser',
                        data: { strIdSession: Session.IDSESSION, user: usu, pass: pass, option: option },
                        error: function (ex) {
                            if (fn_error != null) {
                                fn_error.call(xthat, true);
                            }
                        },
                        beforeSend: function () {
                            $.blockUI({
                                message: '<div align="center"><img src="../../../../../Images/loading2.gif"  width="25" height="25" /> Cargando .... </div>',
                                baseZ: $.app.getMaxZIndex() + 1,
                                css: {
                                    border: 'none',
                                    padding: '15px',
                                    backgroundColor: '#000',
                                    '-webkit-border-radius': '10px',
                                    '-moz-border-radius': '10px',
                                    opacity: .5,
                                    color: '#fff'
                                }
                            });

                        },
                        complete: function () {
                            $.unblockUI();
                        },
                        success: function (response) {
                            console.log(222);
                            if (response.result && response.result == 1) {
                                console.log(333);
                                if (fn_success != null) {
                                    console.log(4444);
                                    console.log(fn_success);
                                    console.log(xthat);
                                    fn_success.call(xthat, true);
                                }
                                $this.close();
                            } else if (response.result == 2 || response.result == 0) {
                                alert('La validacion del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.');
                                if (fn_failled != null) {
                                    fn_failled.call(xthat, true);
                                }
                            } else if (response.result == 3) {
                                alert('Ocurrio un error al Validar el Usuario.');
                                if (fn_error != null) {
                                    fn_error.call(xthat, true);
                                }
                            }
                        }
                    });
                }
            },
            Cancelar: {
                click: function (sender, args) {
                    var $that = this;
                    if (fn_cancel != null) {
                        fn_cancel.call(xthat, false);
                    }
                    $that.close();
                }
            }
        }
    });
}
