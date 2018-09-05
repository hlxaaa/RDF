var orderField = 'id'
var desc = true;
var listSort;
var defaultField = 'id'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var isAdd = true;
var isEnglish = 0;

$(document).ready(function () {
    $('body').delegate('.btn-detail', 'click', function () {
        var id = $(this).parent().data('id')
        location.href = '/content/coachInfo?id=' + id;
    })

    $('body').delegate('.btn-edit', 'click', function () {
        isAdd = false;
        $('#modal-popout').modal('show')
        var id = $(this).parent().data('id')
        var data1 = {
            id: id
        }
        jQuery.postNL('../contentAjax/GetCoachItem', data1, function (data) {
            var user = data.ListData[0];
            $('.headImg').attr('src', user.Url)
            $('.account').val(user.account)
            $('.UserName').val(user.UserName)
            $('.Height').val(user.Height)
            $('.Weight').val(user.Weight)
            $('.Phone').val(user.Phone)
            $('.Birthday').val(user.Birthday)
            $('.UserPwd').val(user.UserPwd)
            $('#coachId').val(user.Id)
            $('.RegisterTime').val(user.RegisterTime)
            $('.BMI').val(user.BMI)
            $('.LoginTime').val(user.LoginTime)
            $('.fhr').val(user.fhr)
            $('.livePrice').val(user.livePrice);
            if (user.Sex) {
                $('.online').click();
            } else {
                $('.offline').click();
            }
        })
    })

    $('.addModel').click(function () {
        isEnglish = 0;
        isAdd = true;
        $('.headImg').attr('src', '../images/new/upload.svg')
        $('.account').val('')
        $('.UserName').val('')
        $('.Height').val(0)
        $('.Weight').val(0)
        $('.Phone').val('')
        $('.Birthday').val('')
        $('.UserPwd').val('')
        $('#coachId').val('')
        $('.RegisterTime').val('')
        $('.BMI').val('')
        $('.LoginTime').val('')
        $('.fhr').val('')
        $('.livePrice').val(0)
    })



    $('.btn-save').click(function () {
        var data1 = GetData();
        if (isAdd) {
            var titleUrl = $('.headImg').attr('src');
            if (titleUrl.substring(0, 4) != 'data') {
                layer.msg('请选择头像')
                return;
            }
            jQuery.postNL('../ContentAjax/AddCoach', data1, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page)
                    }
                })
            })
        } else {
            jQuery.postNL('../ContentAjax/UpdateCoach', data1, function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page)
                    }
                })
            })
        }
    })

    $('.li-content').addClass('open')
    $('.a-coach').addClass('active')

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

    changePage(1);

    $('.headImg').click(function () {
        $('#upImg').val('');
        $('#upImg').click();
    })

    $("#upImg").change(function () {
        var file = this.files[0];
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function (e) {
            $('.headImg').attr('src', this.result);
        };
    });

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

    jQuery.postNL('../contentAjax/GetCoachList', data1, function (data) {

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

            var isPass = item.isPass == null ? "" : item.isPass
            var Enabled = item.Enabled;
            var EnabledHtml = `
    <label class="css-input switch switch-warning">
        <input type="checkbox" checked=""><span class="span-enabled selected" data-id="${id}"></span>
    </label>
`
            if (!Enabled)
                EnabledHtml = `
    <label class="css-input switch switch-warning">
        <input type="checkbox"><span class="span-enabled" data-id="${id}"></span>
    </label>
`
            //var id = item.id; 

            //< td class="center tds td-account" title = "${account}" > ${ account }</td >
            h += `<tr>

<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="center tds td-Sex" title="${Sex}">${Sex}</td>
<td class="center tds td-Phone" title="${Phone}">${Phone}</td>
<td class="center tds td-LoginTime" title="${LoginTime}">${LoginTime}</td>
<td class="text-center">
    <div class="btn-group" data-id=${id}>
        <button class="btn btn-xs btn-default btn-edit" type="button"  title="修改"><i class="fa fa-pencil"></i></button>
   <button class="btn btn-xs btn-default btn-detail" type="button"  title="查看详情" ><i class="si si-magnifier"></i></button>
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
        account: $('.account').val(),
        UserName: $('.UserName').val(),
        Height: $('.Height').val(),
        Sex: RadioCheck(),
        Weight: $('.Weight').val(),
        Phone: $('.Phone').val(),
        Birthday: $('.Birthday').val(),
        UserPwd: $('.UserPwd').val(),
        Id: $('#coachId').val(),
        livePrice: $('.livePrice').val(),
        //isEnglish: isEnglish
    }
    return data;
}
