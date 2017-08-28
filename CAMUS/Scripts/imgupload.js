~(function (window, $) {
    var Picture = function (options) {
        this.options = $.extend({}, Picture.DEFAULTS, options);
        this.init();
    };

    Picture.DEFAULTS = {
        //        mainPicCount: 5,    // 主图默认上传5张
        //        detailPicCount: 0   // 不限制
    };

    Picture.prototype = {
        constructor: Picture,

        init: function () {

            //            this.uploadType = 1; //上传类型，1主图  2详情图
            //            this.uploadMainCount = 0; //主图上传数
            //            this.uploadDetailCount = 0; //详情图上传数

            this.setupEvents();
        },
        setupEvents: function () {
            var that = this;
            $("#fileupload").fileupload({
                dataType: "json",
                add: function (e, data) {
                    that.uploadType = e.target.id === "fileupload_detail" ? 2 : 1;
                    data.submit();
                },
                done: function (e, data) {
                    var result = data.result;
                    if (result.status === 170) {
                        //上传失败
                        // layer.msg(result.msg);
                    }
                    else {
                        var $img = $(".entry-banner-list>.g-add-img");
                        $img.before(that.picTmpl(result.path, result.bannerId));
                    }
                },
                change: function (e, data) {
                    var check = true;
                    $.each(data.files, function (index, file) {
                        if (file.size > 3000000) {
                            check = false;
                        }
                    });
                    if (!check) {
                        layer.msg("上传图片的不能大于3M，请重新上传！", { icon: 0 });
                        return false;
                    }
                }
            });

            $(".entry-banner-list").on({
                mouseenter: function () {
                    $(this).find(".g-img-op").show();
                },
                mouseleave: function () {
                    $(this).find(".g-img-op").hide();
                }
            }, ".g-item");

            $(".entry-banner-list").on("click", ".g-img-op", function () {
                var id = $(this).data("id");
                $.post("/admin/bannerdelete", { id: id }, function () {

                });
                $(this).parent().remove();
            });
        },

        picTmpl: function (path, id) {
            var html = [];
            html.push('<li class="g-item">');
            html.push('   <div class="g-img-info">');
            html.push('     <a href="' + path + '" target="_blank"><img src="' + path + '" /></a>');
            html.push('  </div>');
            html.push('  <div class="g-img-op">');
            html.push('     <a class="" href="javascript:;" data-id="' + id + '" title="删除" ><i class="glyphicon glyphicon-trash"></i></a>');
            html.push('   </div>');
            html.push('</li>');

            return html.join('');
        }
    };

    window.picture = Picture;
}(window, jQuery));