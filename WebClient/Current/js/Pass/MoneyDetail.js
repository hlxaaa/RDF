var orderField = 'id'
var desc = false;
var listSort;
var defaultField = 'id'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var isAdd = true;

$(document).ready(function () {
    $('.Birthday').keypress(function (e) {
        if (e.keyCode == 13)
            changePage(1);
    })

    $('.Birthday').change(function () {
        changePage(1);
    })

    $('body').delegate('.btn-pass', 'click', function () {
        var id = $(this).parent().data('id')

        layer.confirm('确认提现吗？', {
            btn: ['确定', '取消']
        }, function () {
            var data = {
                Id: id
            }
            jQuery.postNL('../passAjax/PassTakeCash', data, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        changePage(page);
                    }
                })
            })

            layer.closeAll('dialog');
        }, function () {
            layer.closeAll('dialog');
        })


    })

    $('.li-money').addClass('open')
    $('.a-moneyDetail').addClass('active')

    changePage(1);

})

function changePage(index) {
    page = index;
    listSort = new Array();

    $('.table th').each(function () {
        listSort.push($(this).attr('class'))
    })
    var data1 = {
        search: $('.input-search').val(),
        pageIndex: index,
        orderField: orderField,
        isDesc: desc,
        size: $('.select-size').val(),
        date: $('.Birthday').val()
        //isEnglish: $('.select-isEnglish').val()
    }

    jQuery.postNL('../passAjax/GetMoneyDetailList', data1, function (data) {

        var index = data.index;
        var pages = data.pages;
        var data = data.ListData;
        if (data.length == 0) {

            if ($('.table th').length > 1) {
                ths = $('.table tr:first').html();
            }
            $('.table tr').remove();

            $('.table tbody').append('<tr class="tr-del"><th>没有相关信息</th></tr>')
            $('.pagination').children().remove();
            return;
        }
        var hh = ''
        if ($('.table th').length < 2) {
            hh += `<tr>${ths}</tr>`;
            $('.table thead').append(hh);
        }
        for (var i = 0; i < data.length; i++) {
            var item = data[i]
            var type = item.type == null ? "" : item.type
            var payUserName = item.payUserName == null ? "" : item.payUserName
            var coachName = item.coachName == null ? "" : item.coachName
            var payType = item.payType == null ? "" : item.payType
            var account = item.account == null ? "" : item.account
            var outTradeNo = item.outTradeNo == null ? "" : item.outTradeNo
            var money = item.money == null ? "" : item.money
            var createTime = item.createTime == null ? "" : item.createTime
            var id = item.id; 


            h += `<tr>
<td class="center tds td-type" title="${id}">${id}</td>
<td class="center tds td-type" title="${type}">${type}</td>
<td class="center tds td-payUserName" title="${payUserName}">${payUserName}</td>
<td class="center tds td-coachName" title="${coachName}">${coachName}</td>
<td class="center tds td-payType" title="${payType}">${payType}</td>
<td class="center tds td-account" title="${account}">${account}</td>
<td class="center tds td-outTradeNo" title="${outTradeNo}">${outTradeNo}</td>
<td class="center tds td-money" title="${money}">${money}</td>
<td class="center tds td-createTime" title="${createTime}">${createTime}</td>

</tr>`

        }
        h += ' </table>'
        $('.tr-del').remove();
        $('.table tbody tr').remove();
        $('.table').append(h);
        $('.pagination').children().remove();
        var h = getPageHtml(pages, index);
        $('.pagination').append(h);

        //tableFixedTest("fixtable")
    })
}

