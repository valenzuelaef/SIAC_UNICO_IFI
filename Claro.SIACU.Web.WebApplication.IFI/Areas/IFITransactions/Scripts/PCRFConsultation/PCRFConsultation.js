(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTPCRFConsultation.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblTitle: $('#lblTitle', $element)
            , lblIMSI: $('#lblIMSI', $element)
            , lblIMSI_Validacion: $('#lblIMSI_Validacion', $element)
            , lblLineaAsociada: $('#lblLineaAsociada', $element)
            , lblLineaAsociada_Validacion: $('#lblLineaAsociada_Validacion', $element)
            , lblPerfilPCRF: $('#lblPerfilPCRF', $element)
            , lblPerfilPCRF_Validacion: $('#lblPerfilPCRF_Validacion', $element)
            , lblCicloFacturacionPCRF: $('#lblCicloFacturacionPCRF', $element)
            , lblCicloFacturacionPCRF_Validacion: $('#lblCicloFacturacionPCRF_Validacion', $element)
            , lblTipoServicio: $('#lblTipoServicio', $element)
            , lblTipoServicio_Validacion: $('#lblTipoServicio_Validacion', $element)
            , lblSPC_Validacion: $('#lblSPC_Validacion', $element)
            , lblSI_Validacion: $('#lblSI_Validacion', $element)
            , lblVelocidadDegradada: $('#lblVelocidadDegradada', $element)
            , lblDatosConsumidos: $('#lblDatosConsumidos', $element)
            , pnlDatosConsumidos: $('#pnlDatosConsumidos', $element)
            , lblObservacion: $('#lblObservacion', $element)
            , lblMensaje: $('#lblMensaje',$element)
            , btnCerrar: $("#btnCerrar", $element)
            //INI - RF-04 - Evalenzs
            , lblUltimoPaqCompradoAdic: $('#lblUltimoPaqCompradoAdic', $element)
            , lblMotDegradacionAdic: $('#lblMotDegradacionAdic', $element)
            , lblVelDegradadaAdic: $('#lblVelDegradadaAdic', $element)
            , lblDatosConsumidosAdic: $("#lblDatosConsumidosAdic", $element)
            , divPaquetesAdicionales: $("#divPaquetesAdicionales",$element)
            
        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();

            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        modelPCRF:{},
        render: function () {
             
            var that = this,
                 controls = that.getControls();
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
        
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            that.loadData();
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


        loadData: function(){ 
            var that = this,
                controls = this.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oDataService = Session.DATASERVICE, 
                oUserAccess = Session.USERACCESS;
        
     
            if (oUserAccess != null) {
                if (!oUserAccess.optionPermissions.indexOf('SU_LTE_VCGB')) {
                    controls.pnlDatosConsumidos.css('visibility', 'hidden');
                }

                that.modelPCRF.strCurrentUser = oUserAccess.Login; //Descomentar
                that.modelPCRF.strNombreClienteSession = oUserAccess.FullName; //Descomentar
            }
            else {
                that.modelPCRF.strCurrentUser = Session.LOGIN
                that.modelPCRF.strNombreClienteSession = ""; //Descomentar
                
            }
            
   
         
            controls.lblTitle.text("CONSULTA PCRF");
            that.modelPCRF.strIdSession = Session.IDSESSION;
          
            that.modelPCRF.strAccountIdSession = oCustomer.ContractID;
            that.modelPCRF.strCicloFacturacionSession = oCustomer.objPostDataAccount.BillingCycle;
           
            that.modelPCRF.strIMSISession = oDataService.NumberIMSI;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(that.modelPCRF),
                url: '/IFITransactions/PCRFConsultation/PageLoad',
          
                success: function (response) {
 
                    if (response != null) {
                        controls.lblIMSI.text((response.data.strIMSI == null ? "" : response.data.strIMSI));
                        if (response.data.blnIMSI_Validacion_Color)
                        {
                            controls.lblIMSI_Validacion.css('color', 'red');
                        }
                        controls.lblIMSI_Validacion.text((response.data.strIMSI_Validacion==null?"":response.data.strIMSI_Validacion));
                        controls.lblLineaAsociada.text((response.data.strLineaAsociada == null ? "" : response.data.strLineaAsociada));
                        if (response.data.blnLineaAsociada_Validacion_Color) {
                            controls.lblLineaAsociada_Validacion.css('color', 'red');
                        }
                        controls.lblLineaAsociada_Validacion.text((response.data.strLineaAsociada_Validacion == null ? "" : response.data.strLineaAsociada_Validacion));
                        controls.lblPerfilPCRF.text((response.data.strPerfilPCRF == null ? "" : response.data.strPerfilPCRF));
                        if (response.data.blnPerfilPCRF_Validacion_Color) {
                            controls.lblPerfilPCRF_Validacion.css('color', 'red');
                        }
                        controls.lblPerfilPCRF_Validacion.text((response.data.strPerfilPCRF_Validacion == null ? "" : response.data.strPerfilPCRF_Validacion));
                        controls.lblCicloFacturacionPCRF.text((response.data.strCicloFacturacionPCRF == null ? "" : response.data.strCicloFacturacionPCRF));
                        if (response.data.blnCicloFacturacionPCRF_Validacion_Color) {
                            controls.lblCicloFacturacionPCRF_Validacion.css('color', 'red');
                        }
                        controls.lblCicloFacturacionPCRF_Validacion.text((response.data.strCicloFacturacionPCRF_Validacion == null ? "" : response.data.strCicloFacturacionPCRF_Validacion));
                        controls.lblTipoServicio.text((response.data.strTipoServicio == null ? "" : response.data.strTipoServicio));
                        if (response.data.blnTipoServicio_Validacion_Color) {
                            controls.lblTipoServicio_Validacion.css('color', 'red');
                        }
                        controls.lblTipoServicio_Validacion.text((response.data.strTipoServicio_Validacion == null ? "" : response.data.strTipoServicio_Validacion));
                        if (response.data.blnSPC_Validacion_Color) {
                            controls.lblSPC_Validacion.css('color', 'red');
                        }
                        controls.lblSPC_Validacion.text((response.data.strSPC_Validacion == null ? "" : response.data.strSPC_Validacion));
                        if (response.data.blnSI_Validacion_Color) {
                            controls.lblSI_Validacion.css('color', 'red');
                        }
                        controls.lblSI_Validacion.text((response.data.strSI_Validacion == null ? "" : response.data.strSI_Validacion));
                        if (response.data.blnVelocidadDegradada_Color)
                        {
                            controls.lblVelocidadDegradada.css('color', 'red');
                        }
                        controls.lblVelocidadDegradada.text((response.data.strVelocidadDegradada == null ? "" : response.data.strVelocidadDegradada));
                        controls.lblDatosConsumidos.text((response.data.strDatosConsumidos == null ? "" : response.data.strDatosConsumidos));
                        controls.lblObservacion.text((response.data.strObservacion == null ? "" : response.data.strObservacion));
                      
                        if (!response.data.blnMensaje)
                        {
                            controls.lblMensaje.css('display', 'none');
                            controls.lblMensaje.css('color', 'red');
                        }
                        controls.lblMensaje.text((response.data.strMensaje == null ? "" : response.data.strMensaje));
                                              

                        controls.lblUltimoPaqCompradoAdic.text((response.data.strUltimoPaqCompradoAdic == null ? "" : response.data.strUltimoPaqCompradoAdic));
                        controls.lblMotDegradacionAdic.text((response.data.strMotDegradacionAdic == null ? "" : response.data.strMotDegradacionAdic));
                        controls.lblVelDegradadaAdic.text((response.data.strVelDegradadaAdic == null ? "" : response.data.strVelDegradadaAdic));
                        controls.lblDatosConsumidosAdic.text((response.data.strDatosConsumidosAdic == null ? "" : response.data.strDatosConsumidosAdic));
                    }
                   
                }
            });         
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

    };


    $.fn.INTPCRFConsultation = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTPCRFConsultation'),
                options = $.extend({}, $.fn.INTPCRFConsultation.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTPCRFConsultation', data);
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

    $.fn.INTPCRFConsultation.defaults = {
    }

    $('#divBody').INTPCRFConsultation();

})(jQuery);
