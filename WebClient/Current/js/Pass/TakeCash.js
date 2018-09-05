var orderField = 'status'
var desc = false;
var listSort;
var defaultField = 'status'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var isAdd = true;

$(document).ready(function () {

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
    $('.a-takecash').addClass('active')

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
        size: $('.select-size').val()
    }

    jQuery.postNL('../passAjax/TakeCashList', data1, function (data) {

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
            var coachName = item.coachName == null ? "" : item.coachName
            var money = item.money == null ? "" : item.money
            var payTypeName = item.payTypeName == null ? "" : item.payTypeName
            var account = item.account == null ? "" : item.account
            var accountName = item.accountName == null ? "" : item.accountName
            var createTime = item.createTime == null ? "" : item.createTime
            var status = item.status == null ? "" : item.status
            var statusMsg = item.statusMsg
            var id = item.id;

            var statusHtml = statusMsg
            if (status == 0)
                statusHtml += `<button class="btn btn-minw btn-success btn-pass" type="button">确认提现</button>`

            h += `<tr><td class="center tds td-coachName" title="${coachName}">${coachName}</td>
<td class="center tds td-money" title="${money}">${money}</td>
<td class="center tds td-payTypeName" title="${payTypeName}">${payTypeName}</td>
<td class="center tds td-account" title="${account}">${account}</td>
<td class="center tds td-account" title="${accountName}">${accountName}</td>
<td class="center tds td-createTime" title="${createTime}">${createTime}</td>
<td class="center tds td-status" title="${statusMsg}" data-id=${id}>${statusHtml}

</td>
</tr>`
        //    `        <button class="btn btn-xs btn-default" type="button"  title=""><i class="fa fa-pencil"></i></button>
        //<button class="btn btn-xs btn-default" type="button" title=""><i class="fa fa-times"></i></button>`
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