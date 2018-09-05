//clear
var orderField = 'id'
var desc = true;
var listSort;
var defaultField = 'id'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''

$(document).ready(function () {

    $('body').delegate('.btn-data', 'click', function () {
        var id = $(this).parent().data('id')
        location.href = `/user/userdata?id=${id}`
    })

    $('.li-data').addClass('open')
    $('.a-userList').addClass('active')

    $('body').delegate('.span-enabled', 'click', function () {
        var span = $(this)
        if (span.hasClass('selected')) {
            span.removeClass('selected')
        } else {
            span.addClass('selected')
        }
        var data1 = {
            Id: span.data('id'),
            Enabled: span.hasClass('selected')
        }
        jQuery.postNL('../contentAjax/ToggleCoach', data1, function (data) {
            layer.msg(data.Message)
        })
    })

    $('.btn-save').click(function () {
        jQuery.postNL('../contentAjax/UpdateUser', GetData(), function (data) {
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    $('#modal-popout').modal('hide')
                    changePage(page);
                }
            })
        })
    })

    changePage(1);

    $('.headImg').click(function () {
        $('#upImg').val('');
        $('#upImg').click();
    })

    $("#upImg").change(function () {
        //console.log(1);
        var file = this.files[0];
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function (e) {
            $('.headImg').attr('src', this.result);
        };
    });

    $('body').delegate('.btn-edit', 'click', function () {
        var id = $(this).parent().data('id');
        $('#modal-popout').modal('show');
        var data = {
            id: id
        }
        jQuery.postNL('../userAjax/GetUserById', data, function (data) {
            var data = data.ListData[0];
            $('.headImg').attr('src', data.Url + '?' + GetTs())
            $('.Id').val(data.Id)
            $('.UserName').val(data.UserName);
            $('.Height').val(data.Height);
            $('.Weight').val(data.Weight)
            $('.Phone').val(data.Phone);
            $('.Birthday').val(data.Birthday);
            $('.RegisterTime').val(data.RegisterTime);
            $('.BMI').val(data.BMI);
            $('.LoginTime').val(data.LoginTime);
            $('.fhr').val(data.fhr);
            $('.UserPwd').val('')
            if (data.Sex) {
                $('.online').click();
            } else {
                $('.offline').click();
            }
        })
    })
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
        isEnglish: $('.select-isEnglish').val()
    }

    jQuery.postNL('../userAjax/GetUserList', data1, function (data) {

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
            var Id = item.Id == null ? "" : item.Id
            var UserName = item.UserName == null ? "" : item.UserName
            var Sex = item.Sex == null ? "" : item.Sex
            var Phone = item.Phone == null ? "" : item.Phone
            var LoginTime = item.LoginTime == null ? "" : item.LoginTime
            var UsePlace = item.UsePlace == null ? "" : item.UsePlace
            var frequency = item.frequency == null ? "" : item.frequency
            var KeyName = item.KeyName == null ? "" : item.KeyName
            var allkm = item.allkm == null ? "" : item.allkm
            var allTime = item.allTime == null ? "" : item.allTime
            var allkal = item.allkal == null ? "" : item.allkal
            var Address = item.Address == null ? "" : item.Address
            //var id = item.id; 
            var Enabled = item.Enabled;
            var EnabledHtml = `
    <label class="css-input switch switch-warning">
        <input type="checkbox" checked=""><span class="span-enabled selected" data-id="${Id}"></span>
    </label>
`
            if (!Enabled)
                EnabledHtml = `
    <label class="css-input switch switch-warning">
        <input type="checkbox"><span class="span-enabled" data-id="${Id}"></span>
    </label>
`

            h += `<tr><td class="center tds td-Id" title="${Id}">${Id}</td>
<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="center tds td-Sex" title="${Sex}">${Sex}</td>
<td class="center tds td-Phone" title="${Phone}">${Phone}</td>
<td class="center tds td-LoginTime" title="${LoginTime}">${LoginTime}</td>
<td class="center tds td-UsePlace" title="${UsePlace}">${UsePlace}</td>
<td class="center tds td-frequency" title="${frequency}">${frequency}</td>
<td class="center tds td-KeyName" title="${KeyName}">${KeyName}</td>
<td class="center tds td-allkm" title="${allkm}">${allkm}</td>
<td class="center tds td-allTime" title="${allTime}">${allTime}</td>
<td class="center tds td-allkal" title="${allkal}">${allkal}</td>
<td class="text-center">
    <div class="btn-group" data-id=${Id}>
        <button class="btn btn-xs btn-default btn-edit" type="button"  title="编辑"><i class="fa fa-pencil"></i></button>
 <button class="btn btn-xs btn-default btn-data" type="button"  title="设备统计"><i class="glyphicon glyphicon-signal"></i></button>
    </div>
</td>
<td>${EnabledHtml}</td>
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

function GetData() {
    var data = {
        Url: $('.headImg').attr('src'),
        UserName: $('.UserName').val(),
        Height: $('.Height').val(),
        Sex: RadioCheck(),
        Weight: $('.Weight').val(),
        Phone: $('.Phone').val(),
        Birthday: $('.Birthday').val(),
        Id: $('.Id').val(),
        UserPwd: $('.UserPwd').val(),
        //isEnglish: isEnglish
        //UserPwd: $('.UserPwd').val(),
        //account: $('.account').val(),
        //livePrice: $('.livePrice').val()
    }
    return data;
}