
function DataGridNone(control, action) {
    //禁用
    $('.easyui-linkbutton').linkbutton('disable');
    //请求地址
    var url = $.validator.format('/{0}/{1}', control, action);
    //打开窗口
    $.post(url, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
            //启用按钮
            $('.easyui-linkbutton').linkbutton('enable');
        } else {
            //清除
            if ($('<div id="dialog-form">').length > 0) $('<div id="dialog-form">').remove();
            //加载
            $('<div id="dialog-form">').appendTo('#body').html(d);
        }
    });
}

function DataGridRow(gID, control, action) {
    //选择网格
    var row = $(gID).datagrid('getSelected');
    if (!row) {
        $.messager.alert('错误提示', '错误：未选定网格数据！', 'error');
        return false;
    }
    console.log(row);
    //禁用
    $('.easyui-linkbutton').linkbutton('disable');
    //请求地址
    var url = $.validator.format('/{0}/{1}', control, action);
    //打开窗口
    $.post(url, { id: row.ID }, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
            $('.easyui-linkbutton').linkbutton('enable');
        } else {
            //清除
            if ($('<div id="dialog-form">').length > 0) $('<div id="dialog-form">').remove();
            //加载
            $('<div id="dialog-form">').appendTo('#body').html(d);
        }
    });
}

function DataGridRows(gID, control, action) {
    //选择网格
    var rows = $(gID).datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('错误提示', '错误：未选定网格数据！', 'error');
        return false;
    }
    //禁用
    $('.easyui-linkbutton').linkbutton('disable');
    //请求地址
    var url = $.validator.format('/{0}/{1}', control, action);
    //打开窗口
    $.post(url, { rows: rows }, function (d) {
        if (d.error) {
            $.messager.alert('错误提示', d.message, 'error');
            $('.easyui-linkbutton').linkbutton('enable');
        } else {
            //清除
            if ($('<div id="dialog-form">').length > 0) $('<div id="dialog-form">').remove();
            //加载
            $('<div id="dialog-form">').appendTo('#body').html(d);
        }
    });
}