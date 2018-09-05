$(document).ready(function () {
    $('.fadeIn').keydown(function (e) {
        if (e.keyCode == 13)
            $('.btn-login').click();
    })

    $('.btn-login').click(function () {
        var account = $('#UId').val();
        if (account == '') {
            layer.msg('请输入账号');
            return;
        }
        var pwd = $('#pwd').val();
        if (pwd == '') {
            layer.msg('请输入密码');
            return;
        }
        var data = {
            UId: account,
            pwd: pwd
        }

        jQuery.postNL('../login/Login', data, function (data) {
            layer.msg("登录成功", {
                time: 500,
                end: function () {
                    switch (parseInt(data.Message)) {
                        case 1:
                            location.href = '/pass/coach'
                            break;
                        case 2:
                            location.href = '/common/index'
                            break;
                        case 3:
                            location.href = '/content/videoList'
                            break;
                        case 4:
                            location.href = '/user/UserAuth'
                            break;
                        case 5:
                            location.href = '/dragmodel/modelList'
                            break;
                        case 6:
                            location.href = '/user/userlist'
                            break;
                        case 7:
                            location.href = '/pass/moneyDetail'
                            break;
                        case 8:
                            location.href = '/gym/gymList'
                            break;
                        case 99:
                            location.href = '/gym/server1'
                            break;
                        default:
                    }


                    //if (data.Message == 99)
                    //    location.href = '/gym/server1'
                    //else
                    //    location.href = '/user/userList'
                }
            })
        })
    })

})