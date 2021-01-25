$(function () {

    var $priority = 0;
    //var _array = [0];
    $("input:text, form").attr("autocomplete", "off");

    $('.date-picker').datepicker({
        changeMonth: true,
        changeYear: true,
        //dateFormat: "dd-mm-yy"
    });

    $(".messagebox").show().delay(1000).fadeOut();

    var CheckMandatory = function () {
        debugger;
        if ($("#ChiefComplaint").length) {
            var $ChiefComplaint = $("#ChiefComplaint").val();
            if ($.trim($ChiefComplaint) == '') {
                alert("Chief complaint should not be empty.");
                return false;
            }
        }

        if ($("#studentAllotmentViewModel_ProcedureNotes").length) {
            var $ProcedureNotes = $("#studentAllotmentViewModel_ProcedureNotes").val();
            if ($.trim($ProcedureNotes) == '') {
               // alert("Procedure Notes should not be empty.");
                return true;
            }
        }

        //return true;
        var result = CheckCheckboxTextMandatory();
        if (result == false) {
            return false;
        }
        return true;
    }

    var CheckCheckboxTextMandatory = function () {
        debugger;
        var valid = true;
        $('.js_deptChk').each(function () {
            //debugger;
            var ischecked = $(this).is(":checked");
            if (ischecked) {
                var txt = $(this).closest('tr').find('.js_ReferredReason').val();
                if (txt == "") {
                    //$(this).parent().siblings().eq(1).text("it is required!");
                    alert("Referred reason should not be empty")
                    return valid = false;

                }
            }
        });
        return valid;
    }

    $('#SaveButton').on('click', function () {
        if (CheckMandatory()) {
            var Con = confirm("Are you sure you want to save ?");
            if (Con == true) {
                var $this = $(this);
                $this.hide();
            }
            else {
                return false;
            }
        }
    });




    $('#UpdateButton').on('click', function () {

        debugger;
        if (CheckMandatory()) {
            var Con = confirm("Are you sure you want to update ?");
            if (Con != true) {
                return false;
            }
        }
    });



    var options = {
        now: "12:35", //hh:mm 24 hour format only, defaults to current time 
        twentyFour: false, //Display 24 hour format, defaults to false 
        upArrow: 'wickedpicker__controls__control-up', //The up arrow class selector to use, for custom CSS 
        downArrow: 'wickedpicker__controls__control-down', //The down arrow class selector to use, for custom CSS 
        close: 'wickedpicker__close', //The close class selector to use, for custom CSS 
        hoverState: 'hover-state', //The hover state class to use, for custom CSS 
        title: 'Time', //The Wickedpicker's title, 
        showSeconds: false, //Whether or not to show seconds, 
        secondsInterval: 1, //Change interval for seconds, defaults to 1  , 
        minutesInterval: 1, //Change interval for minutes, defaults to 1 
        beforeShow: null, //A function to be called before the Wickedpicker is shown 
        show: null, //A function to be called when the Wickedpicker is shown 
        clearable: false, //Make the picker's input clearable (has clickable "x")  
    }; $('.timepicker').wickedpicker(options);


    $('.timepicker').click(function () {
        var thDate = $("#followupViewModal_FollowupTime");
        thDate.attr("readonly", "readonly");
        thDate.datepicker('setDate', new Date());
    });

    $('.input-group-addon').click(function () {
        $(".date-picker", $(this).closest(".input-group")).focus();
    });
    $(".OnlyNumber").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            $("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });


    $(".OnlyCharacters").keypress(function (e) {
        if (e.which == 8 && e.which == 32 && (e.which <= 65 || e.which >= 90)) {
            $("#errmsg").html("Characters Only").show().fadeOut("slow");
            return false;
        }
    });



    $("#ChiefComplaintName").change(function () {
        var dropdowntext = $("#ChiefComplaintName option:selected").text();
        var textboxText = $("#ChiefComplaint").val().trim();
        if (textboxText.indexOf(dropdowntext) >= -1) {

            if (textboxText.length > 0) {
                $("#ChiefComplaint").val(textboxText + " \n" + dropdowntext);
            }
            else {
                $("#ChiefComplaint").val(dropdowntext);
            }
        }

    });

    $("#PresentIllnessName").change(function () {
        var dropdowntext = $("#PresentIllnessName option:selected").text();
        var textboxText = $("#PresentIllness").val().trim();
        if (dropdowntext != 'Select PresentIllness') {
            if (textboxText.length > 0) {
                $("#PresentIllness").val(textboxText + " \n" + dropdowntext);
            }
            else {
                $("#PresentIllness").val(dropdowntext);
            }
        }

    });

    $(".js_Abbr button").click(function () {
        $('#txtSampleAbbreviation').val($(this).attr("data-id"));
    });

    $(".js_Teeth textarea").click(function () {

        var value = $('#txtSampleAbbreviation').val();
        var $this = $(this);
        if ($this.val().length === 0) {
            if (value.length != 0) {
                $this.val(value);
            }
        }
        else {
            var preString = $this.val();

            if (value.length != 0) {
                $this.val($this.val() + ',\n' + value);
            }
            else {
                value = preString.substr(0, value.length - 1);
                $this.val(value);
            }
        }
    });


    $(".js_ShortAbbr button").click(function () {
        $('#txtShortAbbreviation').val($(this).attr("data-id"));
    });

    $(".js_ShortTeeth textarea").click(function () {

        var value = $('#txtShortAbbreviation').val();
        var $this = $(this);
        if ($this.val().length === 0) {
            if (value.length != 0) {
                $this.val(value);
            }
        }
        else {
            var preString = $this.val();

            if (value.length != 0) {
                $this.val($this.val() + ',\n' + value);
            }
            else {
                value = preString.substr(0, value.length - 1);
                $this.val(value);
            }
        }
    });


    $(".js_DecidiousAbbr button").click(function () {
        $('#txtDecidiousAbbreviation').val($(this).attr("data-id"));
    });

    $(".js_DecidiousTeeth textarea").click(function () {

        var value = $('#txtDecidiousAbbreviation').val();
        var $this = $(this);
        if ($this.val().length === 0) {
            if (value.length != 0) {
                $this.val(value);
            }
        }
        else {
            var preString = $this.val();

            if (value.length != 0) {
                $this.val($this.val() + ',\n' + value);
            }
            else {
                value = preString.substr(0, value.length - 1);
                $this.val(value);
            }
        }
    });


    $(".js_DmftAbbr button").click(function () {
        $('#txtSampleAbbreviationDmft').val($(this).attr("data-id"));
    });

    $(".js_Dmft textarea").click(function () {

        var value = $('#txtSampleAbbreviationDmft').val();
        var $this = $(this);
        if ($this.val().length === 0) {
            if (value.length != 0) {
                $this.val(value);
            }
        }
        else {
            var preString = $this.val();

            if (value.length != 0) {
                $this.val($this.val() + ',\n' + value);
            }
            else {
                value = preString.substr(0, value.length - 1);
                $this.val(value);
            }
        }
    });


    $(".js_DeftAbbr button").click(function () {
        $('#txtSampleAbbreviationDeft').val($(this).attr("data-id"));
    });

    $(".js_Deft textarea").click(function () {

        var value = $('#txtSampleAbbreviationDeft').val();
        var $this = $(this);
        if ($this.val().length === 0) {
            if (value.length != 0) {
                $this.val(value);
            }
        }
        else {
            var preString = $this.val();

            if (value.length != 0) {
                $this.val($this.val() + ',\n' + value);
            }
            else {
                value = preString.substr(0, value.length - 1);
                $this.val(value);
            }
        }
    });

    //PHP

    $(function () {

        $(".js_DmftAbbr button").click(function () {
            $('#txtSampleAbbreviation').val($(this).attr("data-id"));
        });



        $(".js_Deftb input[type='text']").click(function () {

            var value = $('#txtSampleAbbreviation').val();
            var $this = $(this);
            if ($this.val().length === 0) {
                if (value.length != 0) {
                    $this.val(value);
                }
            }
            else {
                var preString = $this.val();

                if (value.length != 0) {
                    $this.val($this.val() + ',\n' + value);
                }
                else {
                    value = preString.substr(0, value.length - 1);
                    $this.val(value);
                }
            }
        });

    });

    $('#ApprovalButtonFreeze').on('click', function () {
        debugger;
        var DueAmount = 0;
        DueAmount = $('#studentAllotmentViewModel_DueAmount').val();
        if (DueAmount != "0.00") {
            alert("Sorry!!! Patient has due amount!");
            return false;
        }
        else {
            return true;
        }
    });


    //PHP

    $(".checkbox-inline").click(function () {
        var $this = $(this);
        var $textbox = $this.closest(".form-group").find("input[type='text'], textarea");
        if ($this.is(":checked")) {
            $textbox.removeAttr("disabled");
            $textbox[0].focus();
        } else {
            $textbox.attr("disabled", "disabled");
            $textbox.val("");
        }
    });

    $(".radio-inline").click(function () {
        var $this = $(this);
        var $textbox = $this.closest(".form-group").find("input[type='text'], textarea");
        if ($this.is(":checked")) {
            $textbox.removeAttr("disabled");
            $textbox[0].focus();
        } else {
            $textbox.attr("disabled", "disabled");
            $textbox.val("");
        }
    });

    $(".js_deptChk").click(function () {

        var $this = $(this);
        var $textbox = $this.closest(".js_deptRow").find("input[type='text'], textarea");
        if ($this.is(":checked")) {
            $textbox.removeAttr("disabled");


            var _array = [0];
            $('.js_Priority').each(function () {
                _array.push($(this).closest('tr').find('.js_Priority').val());
            });

            $priority = Math.max.apply(Math, _array) + 1;
            _array.push($priority);
            //_array.reverse();
            var $row = $this.closest(".js_deptRow").find(".js_Priority");
            //$row.attr("disabled", "disabled");
            $row.val($priority);

            var $rowRoomNo = $this.closest(".js_deptRow").find(".js_RoomNo");
            //$rowRoomNo.attr("disabled", "disabled");

            var $rowReason = $this.closest(".js_deptRow").find(".js_ReferredReason");
           // $rowReason.prop('required', true);
            $textbox[1].focus();

        } else {
            $textbox.attr("disabled", "disabled");
            var $rowpriority = $this.closest(".js_deptRow").find(".js_Priority");
            $rowpriority.val("");
            var $rowReason = $this.closest(".js_deptRow").find(".js_ReferredReason");
            $rowReason.val("");
            $rowReason.prop('required', false);
            _array.pop();
            //$textbox.val("");
        }
    });


    if ($("#ReadOnlyApproval1").val() == "True") {
        $('.js_ReadOnlyApproval1 textarea').attr('disabled', true);
        //$('.js_ReadOnlyApproval2 text').attr('readonly', true);
        $('.js_ReadOnlyApproval1 input').attr('readonly', true);
        $('.js_ReadOnlyApproval1 select').attr('disabled', true);
        $('.js_Abbr button').attr('disabled', true);
        $('.js_ReadOnlyApproval1 input[type=checkbox]').attr('disabled', 'true');
        //$('.btnRestroAdd').prop('disabled', true);
    }

    if ($("#EditCaseSheetAccess").val() == "True") {
        $('.js_Abbr button').attr('disabled', false);
    }


    if ($("#ReadOnlyApproval2").val() == "True") {
        debugger;
        $('.js_ReadOnlyApproval2 textarea').attr('readonly', true);
        $('.js_ReadOnlyApproval2 text').attr('readonly', true);
        $('.js_ReadOnlyApproval2 input').attr('readonly', true);
        $('.js_ReadOnlyApproval2 select').attr('disabled', true);
        $('.js_ReadOnlyApproval2 input[type=checkbox]').attr('disabled', 'true');
        $('.btnRestroAdd').hide();
        $('.RestorativeProToothEdit').attr("disabled", "disabled");
        $('.RestorativeProToothdelete').attr("disabled", "disabled");

        $('.btnPostAdd').hide();
        $('.PostAndCoreToothEdit').attr("disabled", "disabled");
        $('.PostAndCoreToothdelete').attr("disabled", "disabled");

        $('.btnSurgPro').hide();
        $('.SurgicalProToothEdit').attr("disabled", "disabled");
        $('.SurgicalProToothdelete').attr("disabled", "disabled");

        $('.btnEstheticCor').hide();
        $('.EstheticCorrToothEdit').attr("disabled", "disabled");
        $('.EstheticCorrToothdelete').attr("disabled", "disabled");

        $('.btnBleach').hide();
        $('.BleachingToothEdit').attr("disabled", "disabled");
        $('.BleachingToothdelete').attr("disabled", "disabled");

        $('.btnTrauma').hide();
        $('.TraumatisedToothEdit').attr("disabled", "disabled");
        $('.TraumatisedToothdelete').attr("disabled", "disabled");

        $('.btnRootCanal').hide();
        $('.RootCanalToothEdit').attr("disabled", "disabled");
        $('.RootCanalToothdelete').attr("disabled", "disabled");

        $('.btnReRCanal').hide();
        $('.ReRootCanalEdit').attr("disabled", "disabled");
        $('.ReRootCanalToothdelete').attr("disabled", "disabled");

        $('.btnIncRoot').hide();
        $('.IncRootFormEdit').attr("disabled", "disabled");
        $('.IncRootFormToothdelete').attr("disabled", "disabled");

    }


    if ($("#ReadOnlyApproval3").val() == "True") {
        debugger;
        $('.js_ReadOnlyApproval3 textarea').attr('readonly', true);
        $('.js_ReadOnlyApproval3 input').attr('readonly', true);
        $('.js_ReadOnlyApproval3 select').attr('readonly', true);
        $('.js_ReadOnlyApproval3 input[type=checkbox]').attr('disabled', 'true');
        $('.js_DmftAbbr button').attr('disabled', true);
        $('.js_DmftAbbr button').attr('disabled', true);
        $('.js_DeftAbbr button').attr('disabled', true);

        $("#btnCalculate").hide();
        $("#CalculateOral").hide();
        $("#CalculateDMFT").hide();
        $("#CalculateDeft").hide();
        $("#btnGiCalculate").hide();


        //$('.RestorativeProToothEdit button').attr('disabled', true);
        //$('.RestorativeProToothdelete button').attr('disabled', true);
        //$('.btnRestroAdd button').attr('disabled', true);
        // $('.btnRestroAdd').prop('disabled', true);
    }

    if ($("#ReadOnlyApproval4").val() == "True") {
        debugger;
        $('.js_ReadOnlyApproval4 textarea').attr('disabled', true);
        $('.js_ReadOnlyApproval4 input').attr('readonly', true);
        $('.js_ReadOnlyApproval4 select').attr('disabled', true);
        $('.js_Abbr button').attr('disabled', true);
        $('.js_DmftAbbr button').attr('disabled', true);

        $('.js_ReadOnlyApproval4 input[type=checkbox]').attr('disabled', 'true');

        $(".hasDatepicker").datepicker('disable');

        $('.btnRestroAdd').hide();
        $('.RestorativeProToothEdit').attr("disabled", "disabled");
        $('.RestorativeProToothdelete').attr("disabled", "disabled");

        $('.btnPostAdd').hide();
        $('.PostAndCoreToothEdit').attr("disabled", "disabled");
        $('.PostAndCoreToothdelete').attr("disabled", "disabled");

        $('.btnSurgPro').hide();
        $('.SurgicalProToothEdit').attr("disabled", "disabled");
        $('.SurgicalProToothdelete').attr("disabled", "disabled");

        $('.btnEstheticCor').hide();
        $('.EstheticCorrToothEdit').attr("disabled", "disabled");
        $('.EstheticCorrToothdelete').attr("disabled", "disabled");

        $('.btnBleach').hide();
        $('.BleachingToothEdit').attr("disabled", "disabled");
        $('.BleachingToothdelete').attr("disabled", "disabled");

        $('.btnTrauma').hide();
        $('.TraumatisedToothEdit').attr("disabled", "disabled");
        $('.TraumatisedToothdelete').attr("disabled", "disabled");

        $('.btnRootCanal').hide();
        $('.RootCanalToothEdit').attr("disabled", "disabled");
        $('.RootCanalToothdelete').attr("disabled", "disabled");

        $('.btnReRCanal').hide();
        $('.ReRootCanalEdit').attr("disabled", "disabled");
        $('.ReRootCanalToothdelete').attr("disabled", "disabled");

        $('.btnIncRoot').hide();
        $('.IncRootFormEdit').attr("disabled", "disabled");
        $('.IncRootFormToothdelete').attr("disabled", "disabled");
    }

    $('#mainId select').prop('disabled', true);

    $("#Rate").change(function () {
        InvestigationCalculation();
    })
    $("#Qty").change(function () {
        InvestigationCalculation();
    })
    $("#DiscountPer").change(function () {
        InvestigationCalculation();
    })

    $("#LabRadRate").change(function () {
        LabRadInvestigationCalculation();
    })
    $("#LabRadQty").change(function () {
        LabRadInvestigationCalculation();
    })
    $("#LabRadDiscountPer").change(function () {
        LabRadInvestigationCalculation();
    })

    $("#DiscountAmt").change(function () {
        InvestigationCalculation();
    })

    $("#ServiceName").change(function () {
        var dropdowntext = $("#ServiceName option:selected").text();
        if (dropdowntext != 'Select') {
            $("#DiscountPer").val(0);
            $("#DiscountAmt").val(0);
            $("#Qty").val(1);
        }
    })



    $("#LabradServiceName").change(function () {
        //debugger
        //var dropdowntext = $("#LabradServiceName option:selected").text();
        //if (dropdowntext != 'Select') {
        $("#LabRadDiscountPer").val(0);
        $("#LabRadDiscountRs").val(0);
        $("#LabRadQty").val(1);
        //}
    })
    $("#SelectLabRad").change(function () {
        //debugger
        var dropdowntext = $("#SelectLabRad option:selected").text();
        var url = rootUrl + "/Investigation/DeleteBillingQueueDetails";
        if (dropdowntext != 'Select') {
            $("#LabRadDiscountPer").val(0);
            $("#LabRadDiscountRs").val(0);
            $("#LabRadQty").val(1);
        }


    })







    var LabRadInvestigationCalculation = function () {
        var Rate = 0;
        var Qty = 0;
        var Amount = 0;
        var DiscountPer = 0;
        var DiscountAmt = 0;
        var NetAmount = 0;

        DiscountPer = $("#LabRadDiscountPer").val();
        if (DiscountPer <= 100) {
            Rate = $("#LabRadRate").val();
            Qty = $("#LabRadQty").val();
            Amount = $("#LabRadAmount").val();
            Amount = Rate * Qty;
            $("#LabRadAmount").val(Amount);
            Amount = $("#LabRadAmount").val();
            DiscountAmt = (Amount * DiscountPer) / 100;
            $("#LabRadDiscountRs").val(DiscountAmt);
            NetAmount = Amount - ((Amount * DiscountPer) / 100);
            $("#LabRadNetAmount").val(NetAmount);
        }
        else {
            Rate = $("#LabRadRate").val();
            $("#LabRadAmount").val(Rate);
            $("#LabRadDiscountPer").val(0);
            $("#LabRadDiscountRs").val(0);
            $("#LabRadNetAmount").val(Rate);
            alert("Discount should not be more than 100%")
        }

    }

    var InvestigationCalculation = function () {

        var Rate = 0;
        var Qty = 0;
        var Amount = 0;
        var DiscountPer = 0;
        var DiscountAmt = 0;
        var NetAmount = 0;

        DiscountPer = $("#DiscountPer").val();
        if (DiscountPer <= 100) {
            Rate = $("#Rate").val();
            Qty = $("#Qty").val();
            Amount = $("#Amount").val();

            //if (Rate > 0)
            //{
            Amount = Rate * Qty;
            $("#Amount").val(Amount);
            Amount = $("#Amount").val();
            DiscountAmt = (Amount * DiscountPer) / 100;
            $("#DiscountAmt").val(DiscountAmt);
            NetAmount = Amount - ((Amount * DiscountPer) / 100);
            $("#NetAmount").val(NetAmount);
            //}
            //else
            //{
            //    alert("Rate Should not be Zero");
            //}

        }
        else {
            Rate = $("#Rate").val();
            $("#Amount").val(Rate);
            $("#DiscountPer").val(0);
            $("#DiscountAmt").val(0);
            $("#NetAmount").val(Rate);
            $("#DiscountAmt").val(0);
            alert("Discount should not be more than 100%")

        }

    }
    $("#btnLabRadAdd").click(function (e) {
        //debugger


        var dropDepttext = $("#ServiceDeptName option:selected").text();
        var dropServicetext = $("#LabRadServiceName option:selected").text();
        var childServiceId = $("#subService").val() == null ? "0" : $("#subService").val();
        if ((dropDepttext != 'Select') && (dropServicetext != 'Select')) {
            var itemIndex = $("#tbLabRadDetails input.iHidden").length;
		    var childServiceItemIndex = $("#tbLabRadDetails input.iHiddenChildService").length;
            e.preventDefault();
            var serviceId = $("#LabRadServiceName").val();
            var ServiceName = $("#LabRadServiceName option:selected").text();
            //var childServiceId = $("#subService").val();4
            var childServiceName = "";
			var isChildService = false;
            if (childServiceId !== "0" && childServiceId !== "") {
                isChildService = true;
			}
			
			if(isChildService){
				childServiceName = $("#subService option:selected").text();
			}


            var amount = $("#LabRadAmount").val();
            var qty = $("#LabRadQty").val();
            var rate = $("#LabRadRate").val();
            var discountPer = $("#LabRadDiscountPer").val();
            if (discountPer == '') {
                discountPer = 0;
            }
            var netAmount = $("#LabRadNetAmount").val();
            var teethNo = $("#LabRadTeethNo").val();
            var billQueueId = 0;
            if ($("#LabRadTotalQty").val() == '') {
                $("#LabRadTotalQty").val(0);
            }
            if ($("#LabRadTotalAmount").val() == '') {
                $("#LabRadTotalAmount").val(0);
            }
            if ($("#LabRadTotalNetAmount").val() == '') {
                $("#LabRadTotalNetAmount").val(0);
            }
            var sumQty = $("#LabRadTotalQty").val();
            var sumAmount = $("#LabRadTotalAmount").val();
            var sumNetAmount = $("#LabRadTotalNetAmount").val();

            var idArray = [];
			$('.iHidden').each(function () {
				idArray.push($(this).val());
			});
			var isExistElement = false;
			if(isChildService == false){				
				if (jQuery.inArray(serviceId, idArray) != '-1') {
					isExistElement = true;
					alert("Already Added");
				}
			}
			else if(isChildService == true){
				var idChildServiceArray = [];
				$('.iHiddenChildService').each(function () {
					idChildServiceArray.push($(this).val());
				});
				if (jQuery.inArray(serviceId, idArray) != '-1' && jQuery.inArray(childServiceId, idChildServiceArray)  != '-1') {
					isExistElement = true;
					alert("Already Added");
				}
			}
            if(!isExistElement) {
                var markupServiceId = "<td><input name='BillingLabRadQueueDetails[" +
                    itemIndex +
                    "].ServiceId' class='form-control iHidden' data-val='true' data-val-number='The field ServiceId must be a number.' data-val-required='The ServiceId field is required.' id='BillingLabRadQueueDetails[" +
                    itemIndex +
                    "]_ServiceId' type='hidden' value=" +
                    serviceId +
                    ">";
                var markupServiceName = "<input type='hidden' id='BillingLabRadQueueDetails[" +
                    itemIndex +
                    "]_ServiceName' class='form-control'  name='BillingLabRadQueueDetails[" +
                    itemIndex +
                    "].ServiceName' value='" +
                    ServiceName +
                    "'></td>";
                var markupChildServiceId = "";
                var markupChildServiceName = "";
                if(isChildService){
                    markupChildServiceId = "<td><input name='BillingLabRadQueueDetails[" +
                        itemIndex +
                        "].ChildServiceId' class='form-control iHiddenChildService' data-val='true' data-val-number='The field ServiceId must be a number.' data-val-required='The ServiceId field is required.' id='BillingLabRadQueueDetails[" +
                        itemIndex +
                        "]_ChildServiceId' type='hidden' value=" +
                        childServiceId +
                        ">";
                    markupChildServiceName = "<input type='hidden' id='BillingLabRadQueueDetails[" +
                        itemIndex +
                        "]_ChildServiceName' class='form-control'  name='BillingLabRadQueueDetails[" +
                        itemIndex +
                        "].ChildServiceName' value='" +
                        childServiceName +
                        "'></td>";
                }


                var markupBillQueueId = "<input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_BillQueueId' class='form-control'  name='BillingLabRadQueueDetails[" + itemIndex + "].BillQueueId' value='" + billQueueId + "'></td>";
                var markupTeethNo = "<td>" + teethNo + "<input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_TeethNo' class='form-control'  name='BillingLabRadQueueDetails[" + itemIndex + "].TeethNo' value='" + teethNo + "'></td>";
                var markupRate = "<td>" + parseFloat(rate).toFixed(2) + "<input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_Rate' class='form-control'  name='BillingLabRadQueueDetails[" + itemIndex + "].Rate' value=" + rate + "> </td>";
                var markupQty = "<td>" + qty + "<input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_Qty' class='form-control js_LRsumQty'  name='BillingLabRadQueueDetails[" + itemIndex + "].Qty' value=" + qty + "> </td>";
                var markupAmount = "<td>" + parseFloat(amount).toFixed(2) + "<input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_LRAmount' class='form-control js_LRsumAmount'  name='BillingLabRadQueueDetails[" + itemIndex + "].Amount' value=" + amount + "> ";
                //var markupDiscountPer = "<td>" + parseFloat(discountPer).toFixed(2) + "<input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_DiscountPer' class='form-control'  name='BillingLabRadQueueDetails[" + itemIndex + "].DiscountPer' value=" + discountPer + "> </td>";
                var markupDiscountPer = "<input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_DiscountPer' class='form-control'  name='BillingLabRadQueueDetails[" + itemIndex + "].DiscountPer' value=" + discountPer + "> </td>";
                var markupNetAmount = "<td>" + parseFloat(netAmount).toFixed(2) + "<input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_NetAmount' class='form-control js_LRsumNetAmount'  name='BillingLabRadQueueDetails[" + itemIndex + "].NetAmount' value=" + netAmount + "> ";
                var markupPayable = "  <input type='hidden' id='BillingLabRadQueueDetails[" + itemIndex + "]_PayableAmount' class='form-control'  name='BillingLabRadQueueDetails[" + itemIndex + "].PayableAmount' value=" + netAmount + "> </td>";

                var deleteButton = "<td> <button type='button' class='LabRadInvestigationdelete btn btn-danger btn-xs' data-id='0'> <i class='glyphicon glyphicon-trash'></i></button> </td>";
                var markupFinal = "<tr>" + markupServiceId + ServiceName + markupBillQueueId + markupServiceName + markupChildServiceId + childServiceName + markupChildServiceName + markupTeethNo + markupRate + markupQty + markupAmount + markupDiscountPer + markupNetAmount + markupPayable + deleteButton + "</tr>";
                $("#tbLabRadDetails tbody").append(markupFinal);
                $('#LabRadTotalQty').val(parseInt(sumQty) + parseInt(qty));
                $('#LabRadTotalAmount').val((parseFloat(sumAmount) + parseFloat(amount)).toFixed(2));
                $('#LabRadTotalNetAmount').val((parseFloat(sumNetAmount) + parseFloat(netAmount)).toFixed(2));
            }
        }
        else {
            $("#LabRadDiscountPer").val(0);
            $("#LabRadDiscountRs").val(0);
            $("#LabRadQty").val(1);
        }


    });



    $("#UpdateButtonApproval1,UpdateButtonApproval2,#UpdateButtonApproval3").on('click', function () {
        debugger;

        var Result = CheckMandatory();
        if (Result != false) {
            var $this = $(this);
            $this.hide();
            return true;
        }
        else { return false; }
    });

    $("#btnInvestigationAdd").click(function (e) {

        var dropdowntext = $("#ServiceName option:selected").text();
        if (dropdowntext != 'Select') {
            var itemIndex = $("#tbInvestigationDetails input.iHidden").length;
            e.preventDefault();
            var serviceId = $("#ServiceName").val();
            var serviceName = $("#ServiceName option:selected").text();
            var amount = $("#Amount").val();
            var qty = $("#Qty").val();
            var rate = $("#Rate").val();
            var discountPer = $("#DiscountPer").val();
            var discountAmt = $("#DiscountAmt").val();
            var netAmount = $("#NetAmount").val();
            var teethNo = $("#TeethNo").val();
            var billQueueId = 0;
            var PayableRequest = "0.00";

            if ($("#TotalQty").val() == '') {
                $("#TotalQty").val(0);
            }
            if ($("#TotalAmount").val() == '') {
                $("#TotalAmount").val(0);
            }
            if ($("#TotalDisAmount").val() == '') {
                $("#TotalDisAmount").val(0);
            }
            if ($("#TotalNetAmount").val() == '') {
                $("#TotalNetAmount").val(0);
            }
            var sumQty = $("#TotalQty").val();
            var sumAmount = $("#TotalAmount").val();
            var sumdisAmount = $("#TotalDisAmount").val();
            var sumNetAmount = $("#TotalNetAmount").val();

            var idArray = [];
            $('.iHidden').each(function () {
                idArray.push($(this).val());
            });
            if (jQuery.inArray(serviceId, idArray) != '-1') {
                alert("Already Added");
            }
            else if (netAmount <= 0) {
                alert("Total should not be Zero");
            }
            else {
                var markupServiceId = "<td><input name='BillingQueueDetails[" + itemIndex + "].ServiceId' class='form-control iHidden' data-val='true' data-val-number='The field ServiceId must be a number.' data-val-required='The ServiceId field is required.' id='BillingQueueDetails[" + itemIndex + "]_ServiceId' type='hidden' value=" + serviceId + ">"
                var markupServiceName = "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_ServiceName' class='form-control'  name='BillingQueueDetails[" + itemIndex + "].ServiceName' value='" + serviceName + "'>";
                var markupBillQueueId = "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_BillQueueId' class='form-control'  name='BillingQueueDetails[" + itemIndex + "].BillQueueId' value='" + billQueueId + "'></td>";
                var markupTeethNo = "<td>" + teethNo + "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_TeethNo' class='form-control'  name='BillingQueueDetails[" + itemIndex + "].TeethNo' value='" + teethNo + "'></td>";
                var markupQty = "<td>" + qty + "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_Qty' class='form-control js_sumQty'  name='BillingQueueDetails[" + itemIndex + "].Qty' value=" + qty + "> </td>";
                var markupAmount = "<td>" + parseFloat(amount).toFixed(2) + "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_Amount' class='form-control js_sumAmount'  name='BillingQueueDetails[" + itemIndex + "].Amount' value=" + amount + "> </td>";
                var markupdisAmount = "<td class='hidden'>" + parseFloat(discountAmt).toFixed(2) + "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_DiscountAmt' class='form-control js_sumdisAmount'  name='BillingQueueDetails[" + itemIndex + "].DiscountAmt' value=" + discountAmt + "> </td>";
                var markupNetAmount = "<td>" + parseFloat(netAmount).toFixed(2) + "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_NetAmount' class='form-control js_sumNetAmount'  name='BillingQueueDetails[" + itemIndex + "].NetAmount' value=" + netAmount + "> ";
                var markupRate = "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_Rate' class='form-control'  name='BillingQueueDetails[" + itemIndex + "].Rate' value=" + rate + "> ";
                var markupDiscountPer = "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_DiscountPer' class='form-control'  name='BillingQueueDetails[" + itemIndex + "].DiscountPer' value=" + discountPer + "> </td>";
                var markupPaid = "<td>" + parseFloat(0).toFixed(2) + " <input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_PaidAmount' class='form-control js_sumPaidAmount'  name='BillingQueueDetails[" + itemIndex + "].PaidAmount' value=0></td>";
                var markupPayable = " <td> <input type='text' maxlength='5' id='BillingQueueDetails[" + itemIndex + "]_PayableAmount' class='form-control js_sumPayableAmount OnlyNumber'  name='BillingQueueDetails[" + itemIndex + "].PayableAmount' value=" + netAmount + "> </td>";
                var markupPayableDisplay = "<td>" + parseFloat(netAmount).toFixed(2) + "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_PayableAmount' class='form-control  js_PayableAmount'  name='BillingQueueDetails[" + itemIndex + "].PayableAmount' value=" + netAmount + "> </td>";
                var markupPayableRequest = "<td class='CenterAlign'>" + PayableRequest + "<input type='hidden' id='BillingQueueDetails[" + itemIndex + "]_PayableRequest' class='form-control js_PayableRequest'  name='BillingQueueDetails[" + itemIndex + "].PayableRequest' value=" + PayableRequest + "> </td>";
                var deleteButton = "<td> <button type='button' class='Investigationdelete btn btn-danger btn-xs' data-id='0'> <i class='glyphicon glyphicon-trash'></i></button> </td>";
                var markupFinal = "<tr>" + markupServiceId + serviceName + markupBillQueueId + markupServiceName + markupTeethNo + markupQty + markupAmount + markupdisAmount + markupNetAmount + markupPaid + markupPayable + markupPayableDisplay + markupPayableRequest + deleteButton + markupRate + markupDiscountPer + "</tr>";



                $("#tbInvestigationDetails tbody").append(markupFinal);
                $('#TotalQty').val(parseInt(sumQty) + parseInt(qty));
                $('#TotalAmount').val((parseFloat(sumAmount) + parseFloat(amount)).toFixed(2));
                $('#TotalDisAmount').val((parseFloat(sumdisAmount) + parseFloat(discountAmt)).toFixed(2));
                $('#TotalNetAmount').val((parseFloat(sumNetAmount) + parseFloat(netAmount)).toFixed(2));

                $("#DiscountAmt").val(0);
            }
        }
        else {
            $("#DiscountPer").val(0);
            $("#DiscountAmt").val(0);
            $("#Qty").val(1);
        }


    });

    var LabRadInvestigationTotalCalculation = function () {
        //debugger
        var sumQty = 0;
        var sumAmount = 0;
        var sumNetAmount = 0;
        $('.js_LRsumQty').each(function () {
            var Qty = $(this);
            sumQty += parseInt(Qty.closest('tr').find('.js_LRsumQty').val());
            sumAmount += parseFloat(Qty.closest('tr').find('.js_LRsumAmount').val());
            sumNetAmount += parseFloat(Qty.closest('tr').find('.js_LRsumNetAmount').val());
        });
        $('#LabRadTotalQty').val(sumQty);
        $('#LabRadTotalAmount').val(parseFloat(sumAmount).toFixed(2))
        $('#LabRadTotalNetAmount').val(parseFloat(sumNetAmount).toFixed(2))

    };

    LabRadInvestigationTotalCalculation();

    var InvestigationTotalCalculation = function () {
        var sumQty = 0;
        var sumAmount = 0;
        var sumNetAmount = 0;
        $('.js_sumQty').each(function () {
            var Qty = $(this);
            sumQty += parseInt(Qty.closest('tr').find('.js_sumQty').val());
            sumAmount += parseFloat(Qty.closest('tr').find('.js_sumAmount').val());
            sumNetAmount += parseFloat(Qty.closest('tr').find('.js_sumNetAmount').val());
        });
        $('#TotalQty').val(sumQty);
        $('#TotalAmount').val(parseFloat(sumAmount).toFixed(2))
        $('#TotalNetAmount').val(parseFloat(sumNetAmount).toFixed(2))
    };

    InvestigationTotalCalculation();





    $("#btnPrescriptionAdd").click(function (e) {

        var dropdowntext = $("#PresTypeId option:selected").text();
        var TypeId = $("#PresTypeId").val();
        var TypeName = $("#PresTypeId option:selected").text();
        var PresMedication = $("#PresMedication").val();
        var PresStrength = $("#PresStrength").val();
        var Qty = $("#PrescriptionQty").val();
        var PresFrequency = $("#PresFrequency").val();
        var PresDays = $("#PresDays").val();
        var PresTimes = $("#PresTimes").val();
        var PresNotes = $("#PresNotes").val();

        if ((dropdowntext != 'Select') && (PresMedication != '') && (Qty != 0)) {
            var itemIndex = $("#tbPrescriptionsDetails input.iHidden").length;
            e.preventDefault();
            var PrescriptionId = 0;
            var idArray = [];
            $('.jcssMedication').each(function () {
                idArray.push($(this).val());
            });
            if (jQuery.inArray(PresMedication, idArray) != '-1') {
                alert("Already Added");
            }
            else {
                var markupPrescriptionId = "<td>" + TypeName + "<input name='PrescriptionsDetails[" + itemIndex + "].PrescriptionId' class='form-control iHidden' data-val='true' data-val-number='The field ServiceId must be a number.' data-val-required='The ServiceId field is required.' id='PrescriptionsDetails[" + itemIndex + "]_PrescriptionId' type='hidden' value=" + PrescriptionId + ">"
                var markupTypeId = "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_TypeId' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].TypeId' value=" + TypeId + "></td>";
                var markupMedication = "<td>" + PresMedication + "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_PresMedication' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].PresMedication' value='" + PresMedication + "'></td>";
                var markupStrength = "<td>" + PresStrength + "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_PresStrength' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].PresStrength' value='" + PresStrength + "'></td>";
                var markupQty = "<td>" + Qty + "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_PrescriptionQty' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].PrescriptionQty' value='" + Qty + "'></td>";
                var markupFrequency = "<td>" + PresFrequency + "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_PresFrequency' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].PresFrequency' value='" + PresFrequency + "'></td>";
                var markupPresDays = "<td>" + PresDays + "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_PresDays' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].PresDays' value='" + PresDays + "'></td>";
                var markupTimes = "<td>" + PresTimes + "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_PresTimes' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].PresTimes' value='" + PresTimes + "'></td>";
                //var markupNotes = "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_PresNotes' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].PresNotes' value='" + PresNotes + "'></td>";
                var markupNotes = "<td>" + PresNotes + "<input type='hidden' id='PrescriptionsDetails[" + itemIndex + "]_PresNotes' class='form-control'  name='PrescriptionsDetails[" + itemIndex + "].PresNotes' value='" + PresNotes + "'></td>";
                var deleteButton = "<td> <button type='button' class='Prescriptionsdelete btn btn-danger btn-xs' data-id='0'> <i class='glyphicon glyphicon-trash'></i></button> </td>";
                var markupFinal = "<tr>" + markupPrescriptionId + markupTypeId + markupMedication + markupStrength + markupQty + markupFrequency + markupPresDays + markupTimes + markupNotes + deleteButton + "</tr>";
                $("#tbPrescriptionsDetails tbody").append(markupFinal);

                $("#PresMedication").val('');
                $("#PresStrength").val('');
                $("#PresFrequency").val('');
                $("#PresDays").val('');
                $("#PrescriptionQty").val(1);
                $("#PresTimes").val('');
                $("#PresNotes").val('');
            }
        }
    });
});

$('#tbInvestigationDetails').on('change', ".js_sumPayableAmount", function () {

    var currentRow = $(this).closest("tr");
    var $NetAmount = currentRow.find("td:eq(5)").text();
    var $PaidAmount = currentRow.find("td:eq(6)").text();
    var payable = $(this).val();
    var $RealPayable = ($NetAmount - $PaidAmount);
    if (payable > $RealPayable) {
        alert("Please Check Payable Amount ");
        $(this).val($RealPayable);
    }
});

