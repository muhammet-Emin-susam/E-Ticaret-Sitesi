/*=========================================================================================
    File Name: app-ecommerce-wishlist.js
    Description: Ecommerce wishlist pages js
    ----------------------------------------------------------------------------------------
    Item Name: Vuexy  - Vuejs, HTML & Laravel Admin Dashboard Template
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

$(function () {
  'use strict';

  var removeItem = $('.remove-wishlist'),
    moveToCart = $('.move-cart'),
    isRtl = $('html').attr('data-textdirection') === 'rtl';

  // remove items from wishlist page
  removeItem.on('click', function () {
    $(this).closest('.ecommerce-card').remove();
    toastr['error']('', 'Ürün Kaldırıldı 🗑️', {
      closeButton: true,
      tapToDismiss: false,
      rtl: isRtl
    });
  });

  // move items to cart
  moveToCart.on('click', function () {
    $(this).closest('.ecommerce-card').remove();
    toastr['success']('', 'Ürün Sepete Eklendi 🛒', {
      closeButton: true,
      tapToDismiss: false,
      rtl: isRtl
    });
  });
});
