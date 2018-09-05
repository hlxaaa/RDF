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
var isAdd = true;
var gymId

$(document).ready(function () {

    $('.addModel').click(function () {
        isAdd = true;
        $('.headImg').attr('src', '../images/new/upload.svg')
        $('.UId').val('')
        $('.UserName').val('')
        $('.UserPwd').val('')
        $('.Phone').val('')
        ClearAuth();
    })

    $('body').delegate('.btn-edit', 'click', function () {
        isAdd = false;
        $('#modal-popout').modal('show')
        var id = $(this).parent().data('id')
        gymId = id;
        var data1 = {
            Id: id
        }
        jQuery.postNL('../userAjax/GetServerUserById', data1, function (data) {
            var user = data.ListData[0];
            $('.headImg').attr('src', user.Url)
            $('.UId').val(user.UId)
            $('.UserName').val(user.UserName)
            $('.Height').val(user.Height)
            $('.Weight').val(user.Weight)
            $('.Phone').val(user.Phone)
            $('.Birthday').val(user.Birthday)
            //$('.UserPwd').val(user.UserPwd)
            $('#coachId').val(user.Id)
            $('.RegisterTime').val(user.RegisterTime)
            $('.BMI').val(user.BMI)
            $('.LoginTime').val(user.LoginTime)
            $('.fhr').val(user.fhr)
            $('.livePrice').val(user.livePrice);
            var arr = user.menuIds;
            SetAuth(arr);
        })
    })

    $('.li-setting').addClass('open')
    $('.a-auth').addClass('active')

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
        jQuery.postNL('../userAjax/ToggleServerUser', data1, function (data) {
            if (data.Message == '') {
                layer.msg("admin账号不能禁用")
                changePage(page)
                return;
            }
            layer.msg(data.Message)
        })
    })

    $('.btn-save').click(function () {

        var r = new Array();
        $('.checkAuth').each(function () {
            var e = $(this);
            if (e.prop('checked')) {
                var menuId = e.data('menuid');
                r.push(menuId)
            }
        })
        if (r.length == 0) {
            layer.msg('至少选择一个权限')
            return;
        }

        if (isAdd) {
            var titleUrl = $('.headImg').attr('src');
            if (titleUrl.substring(0, 4) != 'data') {
                layer.msg('请选择封面图')
                return;
            }
            jQuery.postNL('../userajax/AddServerUser', GetData(), function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page);
                    }
                })
            })
        } else {
            jQuery.postNL('../userajax/UpdateServerUser', GetData(), function (data) {
                layer.msg(data.Message, {
                    time: 500,
                    end: function () {
                        $('#modal-popout').modal('hide')
                        changePage(page);
                    }
                })
            })
        }

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

    jQuery.postNL('../userAjax/GetSeverUserList', data1, function (data) {

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
            var UId = item.UId == null ? "" : item.UId
            var Phone = item.Phone == null ? "" : item.Phone
            var UserName = item.UserName == null ? "" : item.UserName
            var menuNames = item.menuNames == null ? "" : item.menuNames
            var Id = item.Id;
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

            var temp1 = `   <button class="btn btn-xs btn-default btn-edit" type="button"  title="修改"><i class="fa fa-pencil"></i></button>`
            if (Id == 1)
                temp1 = "";

            h += `<tr><td class="center tds td-UId" title="${UId}">${UId}</td>
<td class="center tds td-Phone" title="${Phone}">${Phone}</td>
<td class="center tds td-UserName" title="${UserName}">${UserName}</td>
<td class="center tds td-menuNames" title="${menuNames}">${menuNames}</td>
<td class="text-center">
    <div class="btn-group" data-id=${Id}>
     ${temp1}
 
    </div>
<td>${EnabledHtml}</td>
</td></tr>`
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
        UId: $('.UId').val(),
        UserName: $('.UserName').val(),
        Phone: $('.Phone').val(),
        Id: gymId,
        UserPwd: $('.UserPwd').val(),
        menuIds: GetAuth()
    }
    return data;
}


function GetAuth() {
    var r = new Array();
    $('.checkAuth').each(function () {
        var e = $(this);
        if (e.prop('checked')) {
            var menuId = e.data('menuid');
            r.push(menuId)
        }
    })
    return r;
}

function SetAuth(arr) {
    //ClearAuth()
    $('.checkAuth').each(function () {
        var e = $(this);
        var menuId = e.data('menuid');
        if (arr.indexOf(menuId) >= 0)
            e.prop('checked', true)
        else
            e.prop('checked', false)
    })
}

function ClearAuth() {
    $('.checkAuth').each(function () {
        var e = $(this);
        e.prop('checked', false)
    })
}