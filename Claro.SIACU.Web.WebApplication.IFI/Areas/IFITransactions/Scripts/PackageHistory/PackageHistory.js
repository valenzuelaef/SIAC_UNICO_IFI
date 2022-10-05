(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.PackageHistory.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblTitle: $('#lblTitle', $element)
            , tblPackageHistory: $('#tblPackageHistory', $element)
            , lblCodCliente: $('#lblCodCliente', $element)
            , lblCliente: $('#lblCliente', $element)
            , lblContacto: $('#lblContacto', $element)
            , lblCiclo: $('#lblCiclo', $element)
            , txtfecInicio: $('#txtfecInicio', $element)
            , txtfecFin: $('#txtfecFin', $element)
            , btnBuscar: $('#btnBuscar', $element)
            , btnLimpiar: $('#btnLimpiar', $element)
            , cboTipoPaquete: $('#cboTipoPaquete', $element)
        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();
            console.log("INIT")
            controls.btnLimpiar.addEvent(that, 'click', that.btnLimpiar_Click);
            controls.btnBuscar.addEvent(that, 'click', that.btnBuscar_Click);
            controls.txtfecInicio.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtfecFin.datepicker({ format: 'dd/mm/yyyy' });

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        modelPackageHistory: {},
        objPackageHistory: [],
        render: function () {

            var that = this,
                 controls = that.getControls();
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION == null || SessionTransac.UrlParams.IDSESSION == "" ? "1" : SessionTransac.UrlParams.IDSESSION;

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


        loadData: function () {
            var that = this,
                controls = this.getControls();
            controls.lblTitle.text("HISTÓRICO DE PAQUETES");
            controls.lblCodCliente.html(Session.DATACUSTOMER.Account == '' ? '-' : Session.DATACUSTOMER.Account);
            controls.lblCliente.html(Session.DATACUSTOMER.FullName == ''? '-':Session.DATACUSTOMER.FullName);
            controls.lblContacto.html(Session.DATACUSTOMER.CustomerContact == '' ? '-' : Session.DATACUSTOMER.CustomerContact);
            controls.lblCiclo.html(Session.DATACUSTOMER.objPostDataAccount.BillingCycle == '' ? '-' : Session.DATACUSTOMER.objPostDataAccount.BillingCycle);

            controls.cboTipoPaquete.html("");

            var arrTiposPaquetesHistoricoPaquetes = TiposPaquetesHistoricoPaquetes.split(';');

            $.each(arrTiposPaquetesHistoricoPaquetes, function (index, value) {
                
                controls.cboTipoPaquete.append($('<option>', { value: value.split('|')[0], html: value.split('|')[1] }));
            });

            that.modelPackageHistory.strIdSession = Session.IDSESSION;
            that.modelPackageHistory.strAccountIdSession = Session.DATACUSTOMER.ContractID; // Session.DATASERVICE.NumberIMSI;//descomentar
 
            var Rango = RangoDiasHistoricoPaquetes - -1*1;
            var numDias = '-' + Rango + 'd';

            controls.txtfecInicio.datepicker({ format: 'dd/mm/yyyy', startDate: numDias, endDate: new Date() });// '-1m' -startDate: new Date('2020-07-5'), endDate: new Date() });
            controls.txtfecFin.datepicker({ format: 'dd/mm/yyyy', endDate: new Date() });
            debugger;
            var fecha = new Date();
            fecha.setDate(fecha.getDate() - RangoDiasHistoricoPaquetes)

            controls.txtfecInicio.val(fecha.toLocaleDateString());
            controls.txtfecFin.val(new Date().toLocaleDateString());

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

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(that.modelPackageHistory),
                url: '/IFITransactions/PackageHistory/PageLoad',

                success: function (response) {
                    var objPackage = [];                    
                        var i = 0;
                        $.each(response.data.listaFact, function (index, value) {                            
                            objPackage.push(value);
                            i++;
                        });

                        objPackageHistory = objPackage;
                        that.LoadtblPackageHistory(objPackage);

                    
                    //if (response.data.dataPCRF != null) {
                    //    var i = 0;                        
                    //    $.each(response.data.listaFact, function (index, value) {

                    //        if (i == 0) {
                    //            if (response.data.dataPCRF != null) {
                    //                value.estadoEntrega = response.data.dataPCRF.velocidadDegradada.toUpperCase() == 'NO' ? 'ACTIVO' : 'EXPIRADO';
                    //            } else {
                    //                value.estadoEntrega = 'EXPIRADO';
                    //            }
                    //        }
                    //        else
                    //            value.estadoEntrega = 'EXPIRADO';

                    //        objPackage.push(value);
                    //        i++;
                    //    });


                    //    objPackageHistory = objPackage;
                    //    that.LoadtblPackageHistory(objPackage);
                        

                    //}

                }
            });
        },
        btnLimpiar_Click: function () {
            var that = this,
               controls = this.getControls();
            var fecha = new Date();
            fecha.setDate(fecha.getDate() - RangoDiasHistoricoPaquetes)
            controls.txtfecInicio.val(fecha.toLocaleDateString());
            controls.txtfecFin.val(new Date().toLocaleDateString());

            $("#cboTipoPaquete option[value=" + 'TODOS' + "]").attr("selected", true)
        },
        btnBuscar_Click: function () {
            console.log("EVENTO CLICK BUSCAR")
            var that = this,
                controls = this.getControls();
            debugger;
            var objFilter = [];
            if (controls.txtfecInicio.val() != '' && controls.txtfecInicio.val() != '') {
                var txtFecInicioFormat = controls.txtfecInicio.val().replace(/[^A-Za-z 0-9 \.,\?""!@#\$%\^&\*\(\)-_=\+;:<>\/\\\|\}\{\[\]`~]*/g, '');
                var txtFecFinFormat = controls.txtfecFin.val().replace(/[^A-Za-z 0-9 \.,\?""!@#\$%\^&\*\(\)-_=\+;:<>\/\\\|\}\{\[\]`~]*/g, '');

                var arrtxtfecInicio = txtFecInicioFormat.split('/');
                var arrtxtfecFin = txtFecFinFormat.split('/');
                //var fecInicio = new Date(arrtxtfecInicio[2], arrtxtfecInicio[1] - 1, arrtxtfecInicio[0]);
                //var fecFin =  new Date(arrtxtfecFin[2], arrtxtfecFin[1] - 1, arrtxtfecFin[0]);
                var fecInicio = new Date(parseInt(arrtxtfecInicio[2]), parseInt(arrtxtfecInicio[1]) - 1, parseInt(arrtxtfecInicio[0]));
                var fecFin = new Date(parseInt(arrtxtfecFin[2]), parseInt(arrtxtfecFin[1]) - 1, parseInt(arrtxtfecFin[0]));

                var fecActual = new Date();
                if (fecFin < fecInicio) {
                    alert(" La Fecha Inicial no puede ser mayor a laFecha Final");
                    return false;
                }
                if (fecActual < fecFin) {
                    alert("La Fecha Final no puede ser mayor a la Fecha Actual ");
                    return false;
                }

                
                console.log("fecInicio====>" + fecInicio);
                console.log("fecFin====>" + fecFin);
                $.each(objPackageHistory, function (index, value) {
                    var fecAdquSub = value.fechaAct.substr(0, 10);
                    var arrfecAdquSub = fecAdquSub.split('/');

                    var fecAdqu = new Date(parseInt(arrfecAdquSub[2]), parseInt(arrfecAdquSub[1]) - 1, parseInt(arrfecAdquSub[0]));
                    console.log("fecAdqu====>" + fecAdqu);                   
            
                    if  (fecInicio <= fecAdqu && fecAdqu <= fecFin) {
                        console.log("value====>" + value);
                        objFilter.push(value);
                        if ($('#cboTipoPaquete option:selected').html() != 'TODOS') {
                            objFilter = objFilter.filter(function (x) { return x.tipoPago == $('#cboTipoPaquete option:selected').html() });
                        }
                    }

                });

            } else {
                var fecha = new Date();
                fecha.setDate(fecha.getDate() - RangoDiasHistoricoPaquetes)
                controls.txtfecInicio.val(fecha.toLocaleDateString());
                controls.txtfecFin.val(new Date().toLocaleDateString());

                if ($('#cboTipoPaquete option:selected').html() != 'TODOS') {
                    objFilter = objPackageHistory.filter(function (x) { return x.tipoPago == $('#cboTipoPaquete option:selected').html() });
                }
                else {
                    objFilter = objPackageHistory;
                }
            }


            that.LoadtblPackageHistory(objFilter);
           

        },
        
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        LoadtblPackageHistory: function (data) {
            var that = this;
            var controls = this.getControls();
            controls.tblPackageHistory.DataTable({
                "scrollY": "450px",
                "scrollCollapse": true,
                "paging": true,
                "searching": false,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "select": {
                        "style": "os",
                        "info": false
                    },
                data: data,
                language: {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtrado de  _MAX_ total registros)",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    }
                },
                columns: [
                    { "data": "paquete", "className": "text-center" },
                    { "data": "fechaCPR", "className": "text-center" },
                    { "data": "monto", "className": "text-center" },
                    { "data": "tipoPago", "className": "text-center" },
                    { "data": "fechaAct", "className": "text-center" },
                    { "data": "estado", "className": "text-center" }
                ]
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


    $.fn.PackageHistory = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('PackageHistory'),
                options = $.extend({}, $.fn.PackageHistory.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PackageHistory', data);
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

    $.fn.PackageHistory.defaults = {
    }

    $('#divBody').PackageHistory();

})(jQuery);
