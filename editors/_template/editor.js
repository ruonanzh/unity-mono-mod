// 编辑器逻辑。骨架阶段只验证「页面能起来、three 能用」。
(function () {
  if (!window.THREE) {
    document.body.textContent = "three.bundle.js 没加载到（把它放在本目录）";
    return;
  }
  console.log("three.js", THREE.REVISION);
})();
