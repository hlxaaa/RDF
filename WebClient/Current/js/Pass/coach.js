//clear
var orderField = 'isPass'
var desc = false;
var listSort;
var defaultField = 'isPass'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var isAdd = true;

$(document).ready(function () {

    $('body').delegate('.btn-detail', 'click', function () {
        var id = $(this).parent().data('id');
        location.href = '/pass/coachinfo?id=' + id;
    })

    $('body').delegate('.btn-pass', 'click', function () {
        var id = $(this).parent().data('id')
        var data = {
            Id: id
        }
        jQuery.postNL('../passAjax/PassCoach', data, function (data) {
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    changePage(page);
                }
            })
        })
    })

    $('body').delegate('.btn-notPass', 'click', function () {
        var id = $(this).parent().data('id')
        layer.prompt({ title: '输入驳回理由，并确认', formType: 2 }, function (str, index) {
            layer.close(index);
            var data = {
                isPass: str,
                Id: id
            }
            jQuery.postNL('../passAjax/NotPassCoach', data, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        changePage(page);
                    }
                })
            })
        });
    })

    $('.li-pass').addClass('open')
    $('.a-passCoach').addClass('active')

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
        isEnglish: $('.select-isEnglish').val(),
    }

    jQuery.postNL('../passAjax/GetCoachWaitingList', data1, function (data) {

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
            var account = item.account == null ? "" : item.account
            var UserName = item.UserName == null ? "" : item.UserName
            var Sex = item.Sex == null ? "" : item.Sex
            var Phone = item.Phone == null ? "" : item.Phone
            var LoginTime = item.LoginTime == null ? "" : item.LoginTime
            var id = item.Id;

            var isPass = item.isPass == '0' ? "待审核" : item.isPass

            h += `<tr>

<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="center tds td-Sex" title="${Sex}">${Sex}</td>
<td class="center tds td-Phone" title="${Phone}">${Phone}</td>
<td class="center tds td-isPass" title="${isPass}">${isPass}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
   <button class="btn btn-xs btn-default btn-detail" type="button"  title="查看详情" ><i class="si si-magnifier"></i></button>
    </div>
</td>
<td class="center tds td-" data-id=${id}><button class="btn btn-minw btn-success btn-pass" type="button">通过</button><button class="btn btn-minw btn-danger btn-notPass" type="button">驳回</button></td>
</tr>`
        }
        h += ' </table>'
        $('.tr-del').remove();
        $('.table tbody tr').remove();
        $('.table').append(h);
        $('.pagination').children().remove();
        var h = getPageHtml(pages, index);
        $('.pagination').append(h);
    })
}

