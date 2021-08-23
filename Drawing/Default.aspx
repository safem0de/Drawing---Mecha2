<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Drawing._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <%--<h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
        <h1>Drawing by Engineer</h1>
        <p>เว็บไซต์เพื่อการควบคุมเบิกจ่าย และการใช้ Drawing เพื่อการผลิตอย่างถูกต้อง น้ะจ้ะ</p>
        <img src="Images/EngineerDrawing.png" style="height:30vh;" />
        
        <p>
            <br />
            <a href="~/Search" runat="server" class="btn btn-primary btn-lg"><i class="fas fa-search"></i> เริ่มต้น!! ค้นหา Drawing</a>
            <a href="#" runat="server" class="btn btn-warning btn-lg"><i class="fa fa-check-circle"></i> วิธีการใช้งานเว็บไซต์</a>
            <a href="#" runat="server" class="btn btn-success btn-lg"><i class="fa fa-address-card"></i> ลงทะเบียนเข้าใช้งาน</a>
        </p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>วิธีการใช้งาน</h2>
            <p>
                การเรียกใช้ Drawing ตามใบ WOS การระบุข้อมูล Lot ที่เริ่มต้นใช้งาน Drawing ...
            </p>
            <p>
                <a class="btn btn-default" href="~/WorkInstruction" runat="server">เพิ่มเติม... &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>ลงทะเบียน (Engineer)</h2>
            <p>
                เพื่อควบคุมเอกสาร และระบุตัวตน สำหรับการสอบกลับการดำเนินการทางเอกสาร ...
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">เพิ่มเติม... &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>เกี่ยวกับผู้จัดทำ</h2>
            <p>
                การติดต่อผู้จัดทำเว็บไซต์ รายละเอียดการอัพเดท เว็บไซต์แต่ละครั้ง ...
            </p>
            <p>
                <a class="btn btn-default" href="~/About" runat="server">เพิ่มเติม... &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
