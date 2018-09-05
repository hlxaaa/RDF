function GetParamIdOnAddress() {
    return window.location.search.split('=')[1];
}

$(document).ready(function () {

    //$('.spwd').focus(function () {
    //    $(this).attr('type','password')
    //})

    $('#btn-setPwd').click(function () {
        $('#modal-setPwd').modal('show')
    })

    $('.btn-save-setPwd').click(function () {
        var data1 = {
            spwd: $('.spwd').val(),
            newpwd: $('.newpwd').val(),
            newpwd2: $('.newpwd2').val()
        }
        jQuery.postNL('../settingAjax/setPwd', data1, function (data) {
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    location.reload();
                }
            })
        })
    })

    $('#btn-loginOut').click(function () {
        jQuery.postNL('../login/LoginOut', "{}", function (data) {
            layer.msg(data.Message, {
                time: 500,
                end: function () {
                    location.href = '/user/userList'
                }
            })
        })
    })

    $('.input-search').keydown(function (e) {
        if (e.keyCode == 13)
            changePage(1);
    })

    $('#modal-popout').keydown(function (e) {
        if (e.keyCode == 13)
            $('.btn-save').click();
    })

    $('#modal-edit').keydown(function (e) {
        if (e.keyCode == 13)
            $('.btn-update').click();
    })


    $('.select-isEnglish').change(function () {
        changePage(1);
    })

    $('.select-size').change(function () {
        changePage(1);
    })

    $('body').delegate('.table th', 'click', function () {
        var e = $(this);
        if ($(this).hasClass('sorting')) {
            $('.table .th-sort').removeClass('sorting')
            $('.table .th-sort').removeClass('sorting_asc')
            $('.table .th-sort').removeClass('sorting_desc')
            $('.table .th-sort').addClass('sorting')
            $(this).removeClass('sorting')
            $(this).addClass('sorting_asc')


            //$('.table .th-sort').attr('class', 'th-sort sorting');
            //$(this).attr('class', 'th-sort sorting_asc');

            orderField = e.data('sort');
            desc = true;
            //layer.msg(orderField)
            changePage(1);
        } else if ($(this).hasClass('sorting_asc')) {
            $('.table .th-sort').removeClass('sorting')
            $('.table .th-sort').removeClass('sorting_asc')
            $('.table .th-sort').removeClass('sorting_desc')
            $('.table .th-sort').addClass('sorting')
            $(this).removeClass('sorting')
            $(this).addClass('sorting_desc')
            //$('.table .th-sort').attr('class', 'th-sort sorting');
            //$(this).attr('class', 'th-sort sorting_desc');
            orderField = e.data('sort');
            desc = false;
            //layer.msg(orderField)
            changePage(1);
        }
        else if ($(this).hasClass('sorting_desc')) {
            $('.table .th-sort').removeClass('sorting')
            $('.table .th-sort').removeClass('sorting_asc')
            $('.table .th-sort').removeClass('sorting_desc')
            $('.table .th-sort').addClass('sorting')
            $(this).removeClass('sorting')
            $(this).addClass('sorting_asc')
            //$('.table .th-sort').attr('class', 'th-sort sorting');
            //$(this).attr('class', 'th-sort sorting');
            //orderField = defaultField;
            orderField = e.data('sort');
            desc = true;
            //layer.msg(orderField)
            changePage(1);
        }
    });

    $('body').delegate('.layui-layer-input', 'keypress', function (e) {
        if (e.keyCode == 13)
            $('.layui-layer-btn0').click();
    })

})


function modalHelper() {
    $('.modal-open.modal').css('overflow-y', 'hidden')
    $('.modal-open.modal').css('overflow-y', 'auto')
}


//2选1的按钮，得到true还是false
function RadioCheck() {
    if ($('.online:checked').val() === 'on')
        return true;
    else if ($('.offline:checked').val() === 'on')
        return false;
}

function getPageHtml(pages, thePage) {
    var h = '';
    if (pages <= 5) {
        if (thePage == 1)
            h += ` <li class="paginate_button disabled">
                                <a =""><i class="fa fa-angle-left"></i></a>
                            </li>`
        else
            h += ` <li class="paginate_button " onclick="getPrePage()">
                                <a =""><i class="fa fa-angle-left"></i></a>
                            </li>`

        for (var i = 1; i < pages + 1; i++) {
            if (i == thePage) {
                h += `<li class="paginate_button active"><a>${i}</a></li>`
            }
            else
                h += `<li class="paginate_button" onclick="changePage(${i})"><a>${i}</a></li>`
        }
        if (pages == thePage)
            h += `    <li class="paginate_button disabled">
                                <a><i class="fa fa-angle-right"></i></a>
                            </li>`
        else
            h += `    <li class="paginate_button " onclick="getNextPage()">
                                <a><i class="fa fa-angle-right"></i></a>
                            </li>`
    } else {
        if (thePage <= 2) {
            if (thePage == 1)
                h += ` <li class="paginate_button disabled">
                                <a =""><i class="fa fa-angle-left"></i></a>
                            </li>`
            else
                h += ` <li class="paginate_button " onclick="getPrePage()">
                                <a =""><i class="fa fa-angle-left"></i></a>
                            </li>`

            for (var i = 1; i < pages + 1; i++) {
                if (i == pages) {
                    h += `<li class="paginate_button" onclick="getMidPage(this)"><a>...</a></li>`
                }
                if (i <= thePage + 2 || i == pages) {
                    if (i == thePage)
                        h += `<li class="paginate_button active"><a>${i}</a></li>`
                    else
                        h += `<li class="paginate_button" onclick="changePage(${i})"><a>${i}</a></li>`
                }
            }

            if (pages == thePage)
                h += `    <li class="paginate_button disabled">
                                <a><i class="fa fa-angle-right"></i></a>
                            </li>`
            else
                h += `    <li class="paginate_button " onclick="getNextPage()">
                                <a><i class="fa fa-angle-right"></i></a>
                            </li>`
        } else {
            if (thePage < 4) {
                if (thePage == 1)
                    h += ` <li class="paginate_button disabled">
                                <a =""><i class="fa fa-angle-left"></i></a>
                            </li>`
                else
                    h += ` <li class="paginate_button " onclick="getPrePage()">
                                <a =""><i class="fa fa-angle-left"></i></a>
                            </li>`

                for (var i = 1; i < pages + 1; i++) {
                    if (i == pages && thePage < pages - 3) {
                        h += `<li class="paginate_button" onclick="getMidPage(this)"><a>...</a></li>`
                    }
                    if (i <= thePage + 2 && i >= thePage - 2 || i == pages) {
                        if (i == thePage) {
                            h += `<li class="paginate_button active"><a>${i}</a></li>`
                        }
                        else
                            h += `<li class="paginate_button" onclick="changePage(${i})"><a>${i}</a></li>`
                    }
                }
                if (pages == thePage)
                    h += `    <li class="paginate_button disabled">
                                <a><i class="fa fa-angle-right"></i></a>
                            </li>`
                else
                    h += `    <li class="paginate_button " onclick="getNextPage()">
                                <a><i class="fa fa-angle-right"></i></a>
                            </li>`
            } else {
                if (thePage == 1)
                    h += ` <li class="paginate_button disabled">
                                <a =""><i class="fa fa-angle-left"></i></a>
                            </li>`
                else
                    h += ` <li class="paginate_button " onclick="getPrePage()">
                                <a =""><i class="fa fa-angle-left"></i></a>
                            </li>`

                for (var i = 1; i < pages + 1; i++) {
                    if (i == pages && thePage < pages - 3) {
                        h += `<li class="paginate_button" onclick="getMidPage(this)"><a>...</a></li>`
                    }
                    if (i <= thePage + 2 && i >= thePage - 2 || i == pages || i == 1) {
                        if (i == thePage) {
                            h += `<li class="paginate_button active"><a>${i}</a></li>`
                        }
                        else
                            h += `<li class="paginate_button" onclick="changePage(${i})"><a>${i}</a></li>`
                    }
                    if (i == 1 && thePage > 4) {
                        h += `<li class="paginate_button" onclick="getMidPage(this)"><a>...</a></li>`
                    }
                }

                if (pages == thePage)
                    h += `    <li class="paginate_button disabled">
                                <a><i class="fa fa-angle-right"></i></a>
                            </li>`
                else
                    h += `    <li class="paginate_button " onclick="getNextPage()">
                                <a><i class="fa fa-angle-right"></i></a>
                            </li>`
            }
        }
    }
    if (pages < 1) {
        return "";
    }
    return h;
}

function getPrePage() {
    //console.log(1);
    $('.pagination li').each(function () {
        if ($(this).hasClass('active')) {
            var a = $(this).children('a').text();
            var pre = parseInt(a) - 1;
            changePage(pre);
            return;
        }
    })
}

function getMidPage(node) {
    var prev = node.previousSibling.innerText
    var next = node.nextSibling.innerText
    var thePage = (parseInt(prev) + parseInt(next)) / 2;
    changePage(parseInt(thePage));
}

function getNextPage() {
    $('.pagination li').each(function () {
        if ($(this).hasClass('active')) {
            var a = $(this).children('a').text();
            var next = parseInt(a) + 1;
            changePage(next);
            return false;
        }
    })
}

function encode(str) {
    str = str.trim();
    str = str.replace(/</g, "*lt;");
    str = str.replace(/>/g, "*gt;");
    str = str.replace(/&/g, "*amp");
    return str;
}

function LayerConfirm() {
    layer.confirm('确认删除吗？', {
        btn: ['确定', '取消']
    }, function () {


        layer.closeAll('dialog');
    }, function () {
        layer.closeAll('dialog');
    })
}

function LayerPrompt() {
    layer.prompt({ title: '输入操作密码，并确认', formType: 1 }, function (pass, index) {
        layer.close(index);
        var data = {
            pwd: pass,
            oper: $('.btn-valve').val(),
            commNo: commNo
        }
        jQuery.postNL('../dataAjax/ValveOper', data, function (data) {
            layer.msg(data.Message);
            changePage(page)
        })
    });
}

//function changeWangEditor() {
//    var e = $('.wangEditor-container .wangEditor-menu-container')
//    e.find('.menu-group:eq(0)').remove();
//    e.find('.menu-group:eq(2)').remove();
//    e.find('.menu-group:eq(2) .menu-item:eq(1)').remove();
//    e.find('.menu-group:eq(2) .menu-item:eq(1)').remove();
//    e.find('.menu-group:eq(2) .menu-item:eq(1)').remove();
//    e.find('.menu-group:eq(2) .menu-item:eq(1)').remove();
//}

function GetTs() {
    return Date.parse(new Date());
}