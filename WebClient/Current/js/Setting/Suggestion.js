var orderField = 'id'
var desc = true;
var listSort;
var defaultField = 'id'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var videoId;

var isAdd = true;

$(document).ready(function () {
    $('.li-setting').addClass('open')
    $('.a-suggestion').addClass('active')
    $('body').delegate('.btn-reply', 'click', function () {
        var id = $(this).parent().data('id')
        layer.prompt({ title: '输入回复，并确认', formType: 2 }, function (pass, index) {
            layer.close(index);
            var data = {
                reply: pass,
                Id: id
            }
            jQuery.postNL('../settingAjax/Reply', data, function (data) {
                //var data = data.ListData[0]
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        changePage(page)
                    }
                })
            })
        });

    })

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
        modelType: $('.select-type').val(),
        isEnglish: $('.select-isEnglish').val()
    }

    jQuery.postNL('../settingAjax/GetFeedBackList', data1, function (data) {

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
            var UserName = item.username == null ? "" : item.username
            var Title = item.Title == null ? "" : item.Title
            var msg = item.Content == null ? "" : item.Content
            var createTime = item.EditTime == null ? "" : item.EditTime
            var reply = item.reply == null ? "" : item.reply
            var id = item.Id;

            h += `<tr><td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="center tds td-Title" title="${Title}">${Title}</td>
<td class="center tds td-msg" title="${msg}">${msg}</td>
<td class="center tds td-createTime" title="${createTime}">${createTime}</td>
<td class="center tds td-reply" title="${reply}">${reply}</td>
<td class="text-center">
    <div class="btn-group" data-id="${id}">
        <button class="btn btn-xs btn-default btn-reply" type="button" title="回复"><i class="fa fa-pencil"></i></button>
    </div>
</td>
</tr>`
            //            h += `<tr><td class="center tds td-TitleUrl"><img class="table-img" src="${TitleUrl}?${GetTs()}"></td>
            //<td class="center tds td-Title" title="${Title}">${Title}</td>
            //<td class="center tds td-LongTime" title="${LongTime}">${LongTime}</td>
            //<td class="center tds td-EditTime" title="${EditTime}">${EditTime}</td>
            //<td class="center tds td-PlayCount" title="${PlayCount}">${PlayCount}</td>
            //<td class="center tds td-price" title="${price}">${price}</td>
            //<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
            //<td class="text-center">
            //    <div class="btn-group" data-id=${id}>
            //        <button class="btn btn-xs  btn-default btn-playVideo" type="button"  data-url="${url}"><i class="fa fa-play"></i></button>
            //   <button class="btn btn-xs btn-default btn-edit " type="button"  title=""><i class="fa fa-pencil"></i></button>
            //        <button class="btn btn-xs btn-default" type="button" title=""><i class="fa fa-times"></i></button>
            //    </div>
            //</td>
            //<td>${EnabledHtml}</td>
            //</tr>`
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
