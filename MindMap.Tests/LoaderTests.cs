using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Xunit;

using Mindmap.Domain;
using Mindmap.Persistence;

namespace Mindmap.Tests
{
    
    public class LoaderTests
    {
        [Fact]
        public void Can_build_tree_from_test_document()
        {
            var xml = XDocument.Parse(TestDoc1);

            var target = new ActiveOneNotePageLoader();

            var root = target.ParsePageData(xml);

            Assert.NotNull(root);

            Assert.Equal(PageTitle, root.Title);

            Assert.Equal(3, root.Children.Count);

            Assert.Equal(2, root.RightChildren.Count);

            Assert.Single(root.LeftChildren);

            Assert.Equal(Heading_1_1, root.Children[0].Title);
            Assert.Equal(Heading_1_1, root.RightChildren[0].Title);

            Assert.Equal(Heading_2_1, root.Children[1].Title);
            Assert.Equal(Heading_2_1, root.RightChildren[1].Title);

            Assert.Equal(Heading_3, root.Children[2].Title);
            Assert.Equal(Heading_3, root.LeftChildren[0].Title);
        }

        #region Test Data

        private const string PageTitle = "Test Document for Mind Map";

        private const string Heading_1_1 = "Heading 1.1";
        private const string Heading_1_2 = "Heading 1.2";
        private const string Heading_1_3 = "Heading 1.3";
        private const string Heading_1_4 = "Heading 1.4";
        private const string Heading_1_5 = "Heading 1.5";
        private const string Heading_1_6 = "Heading 1.6";
        private const string Heading_2_1 = "Heading 2.1";
        private const string Heading_2_2 = "Heading 2.2";
        private const string Heading_2_3 = "Heading 2.3";
        private const string Heading_3 = "Heading 3";


        private readonly string TestDoc1 = $@"<?xml version='1.0'?>
<one:Page xmlns:one='http://schemas.microsoft.com/office/onenote/2013/onenote' ID='{{76405033-08D0-4410-86DB-8558C42EF324}}{{1}}{{E19478600825413765754820175553167897833087231}}' name='Test Document for Mind Map' dateTime='2018-03-15T15:18:42.000Z' lastModifiedTime='2018-03-15T16:33:24.000Z' pageLevel='1' isCurrentlyViewed='true' lang='en-GB'>
  <one:QuickStyleDef index='0' name='PageTitle' fontColor='automatic' highlightColor='automatic' font='Calibri Light' fontSize='20.0' spaceBefore='0.0' spaceAfter='0.0' />
  <one:QuickStyleDef index='1' name='p' fontColor='automatic' highlightColor='automatic' font='Calibri' fontSize='11.0' spaceBefore='0.0' spaceAfter='0.0' />
  <one:QuickStyleDef index='2' name='h1' fontColor='#1E4E79' highlightColor='automatic' font='Calibri' fontSize='16.0' spaceBefore='0.0' spaceAfter='0.0' />
  <one:QuickStyleDef index='3' name='h2' fontColor='#2E75B5' highlightColor='automatic' font='Calibri' fontSize='14.0' spaceBefore='0.0' spaceAfter='0.0' />
  <one:QuickStyleDef index='4' name='h3' fontColor='#5B9BD5' highlightColor='automatic' font='Calibri' fontSize='12.0' spaceBefore='0.0' spaceAfter='0.0' />
  <one:QuickStyleDef index='5' name='code' fontColor='automatic' highlightColor='automatic' font='Consolas' fontSize='11.0' spaceBefore='0.0' spaceAfter='0.0' />
  <one:QuickStyleDef index='6' name='h4' fontColor='#5B9BD5' highlightColor='automatic' font='Calibri' fontSize='12.0' italic='true' spaceBefore='0.0' spaceAfter='0.0' />
  <one:QuickStyleDef index='7' name='h5' fontColor='#2E75B5' highlightColor='automatic' font='Calibri' fontSize='11.0' spaceBefore='0.0' spaceAfter='0.0' />
  <one:QuickStyleDef index='8' name='h6' fontColor='#2E75B5' highlightColor='automatic' font='Calibri' fontSize='11.0' italic='true' spaceBefore='0.0' spaceAfter='0.0' />
  <one:PageSettings RTL='false' color='automatic'>
    <one:PageSize>
      <one:Automatic />
    </one:PageSize>
    <one:RuleLines visible='false' />
  </one:PageSettings>
  <one:Title lang='en-GB'>
    <one:OE author='Dave Warry' authorInitials='DW' authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedBy='Dave Warry' lastModifiedByInitials='DW' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:19:01.000Z' lastModifiedTime='2018-03-15T15:19:01.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{15}}{{B0}}' alignment='left' quickStyleIndex='0'>
      <one:T><![CDATA[{PageTitle}]]></one:T>
    </one:OE>
  </one:Title>
  <one:Outline author='Dave Warry' authorInitials='DW' lastModifiedBy='Dave Warry' lastModifiedByInitials='DW' lastModifiedTime='2018-03-15T16:33:24.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{36}}{{B0}}'>
    <one:Position x='36.0' y='68.4000015258789' z='0' />
    <one:Size width='500.0249938964844' height='340.1532287597656' />
    <one:Indents>
      <one:Indent level='0' indent='2.65465970562583E-28' />
    </one:Indents>
    <one:OEChildren>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:20:07.000Z' lastModifiedTime='2018-03-15T15:20:07.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{37}}{{B0}}' alignment='left' quickStyleIndex='1'>
        <one:T><![CDATA[This is a test document for the MindMap viewer. It finds the active OneNote window, and generates a tree of nodes from the page contents:]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:20:13.000Z' lastModifiedTime='2018-03-15T15:20:13.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{41}}{{B0}}' alignment='left' quickStyleIndex='1'>
        <one:T><![CDATA[]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:21:05.000Z' lastModifiedTime='2018-03-15T15:21:05.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{39}}{{B0}}' alignment='left' quickStyleIndex='2'>
        <one:T><![CDATA[{Heading_1_1}]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:23:59.000Z' lastModifiedTime='2018-03-15T15:23:59.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{74}}{{B0}}' alignment='left' quickStyleIndex='1'>
        <one:T><![CDATA[This is a paragraph under Heading 1.1]]></one:T>
        <one:OEChildren>
          <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:49:09.000Z' lastModifiedTime='2018-03-15T15:49:09.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{76}}{{B0}}' alignment='left' quickStyleIndex='1'>
            <one:List>
              <one:Bullet bullet='2' fontSize='11.0' />
            </one:List>
            <one:T><![CDATA[Here's a bulleted list]]></one:T>
          </one:OE>
          <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:49:14.000Z' lastModifiedTime='2018-03-15T15:49:14.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{80}}{{B0}}' alignment='left' quickStyleIndex='1'>
            <one:List>
              <one:Bullet bullet='2' fontSize='11.0' />
            </one:List>
            <one:T><![CDATA[With a second point]]></one:T>
          </one:OE>
        </one:OEChildren>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:21:06.000Z' lastModifiedTime='2018-03-15T15:21:06.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{44}}{{B0}}' alignment='left' quickStyleIndex='3'>
        <one:T><![CDATA[{Heading_1_2}]]></one:T>
        <one:OEChildren>
          <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:49:42.000Z' lastModifiedTime='2018-03-15T15:49:42.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{83}}{{B0}}' alignment='left' quickStyleIndex='1'>
            <one:List>
              <one:Number numberSequence='0' numberFormat='##.' fontSize='11.0' font='Calibri' language='2057' text='1.' />
            </one:List>
            <one:T><![CDATA[Here's a numbered list]]></one:T>
          </one:OE>
          <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:49:50.000Z' lastModifiedTime='2018-03-15T15:49:50.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{87}}{{B0}}' alignment='left' quickStyleIndex='1'>
            <one:List>
              <one:Number numberSequence='0' numberFormat='##.' fontSize='11.0' font='Calibri' language='2057' text='2.' />
            </one:List>
            <one:T><![CDATA[Blah blah blah]]></one:T>
          </one:OE>
        </one:OEChildren>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:55:26.000Z' lastModifiedTime='2018-03-15T15:55:26.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{90}}{{B0}}' alignment='left' quickStyleIndex='1'>
        <one:T><![CDATA[And another paragraph.]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:21:51.000Z' lastModifiedTime='2018-03-15T15:21:51.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{47}}{{B0}}' alignment='left' quickStyleIndex='4'>
        <one:T><![CDATA[{Heading_1_3}]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:56:10.000Z' lastModifiedTime='2018-03-15T15:56:10.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{93}}{{B0}}' alignment='left' quickStyleIndex='5'>
        <one:T><![CDATA[def foo(x, y, z, *args, **kwargs):]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:56:47.000Z' lastModifiedTime='2018-03-15T15:56:47.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{98}}{{B0}}' alignment='left' quickStyleIndex='5'>
        <one:T><![CDATA[    # here's a comment]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:56:19.000Z' lastModifiedTime='2018-03-15T15:56:19.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{96}}{{B0}}' alignment='left' quickStyleIndex='5'>
        <one:T><![CDATA[    pass]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:22:03.000Z' lastModifiedTime='2018-03-15T15:22:03.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{49}}{{B0}}' alignment='left' quickStyleIndex='6'>
        <one:T><![CDATA[<span
style='font-style:italic'>{Heading_1_4}</span>]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:22:23.000Z' lastModifiedTime='2018-03-15T15:22:23.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{51}}{{B0}}' alignment='left' quickStyleIndex='7'>
        <one:T><![CDATA[{Heading_1_5}]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:22:31.000Z' lastModifiedTime='2018-03-15T15:22:31.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{53}}{{B0}}' alignment='left' quickStyleIndex='8'>
        <one:T><![CDATA[<span
style='font-style:italic'>{Heading_1_6}</span>]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:22:46.000Z' lastModifiedTime='2018-03-15T15:22:46.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{55}}{{B0}}' alignment='left' quickStyleIndex='2'>
        <one:T><![CDATA[{Heading_2_1}]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:22:58.000Z' lastModifiedTime='2018-03-15T15:22:58.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{57}}{{B0}}' alignment='left' quickStyleIndex='3'>
        <one:T><![CDATA[{Heading_2_2}]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T15:23:37.000Z' lastModifiedTime='2018-03-15T15:23:37.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{61}}{{B0}}' alignment='left' quickStyleIndex='4'>
        <one:T><![CDATA[{Heading_2_3}]]></one:T>
      </one:OE>
      <one:OE authorResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' lastModifiedByResolutionID='&lt;resolutionId provider=&quot;Windows Live&quot; hash=&quot;7ZNqXka6/W2lXVfi6asxYA==&quot;&gt;&lt;localId cid=&quot;52c19880ddde23db&quot;/&gt;&lt;/resolutionId&gt;' creationTime='2018-03-15T16:33:24.000Z' lastModifiedTime='2018-03-15T16:33:24.000Z' objectID='{{F4A3371D-B931-4668-8D9C-62B2D7793F64}}{{100}}{{B0}}' alignment='left' quickStyleIndex='2'>
        <one:T><![CDATA[{Heading_3}]]></one:T>
      </one:OE>
    </one:OEChildren>
  </one:Outline>
</one:Page>";
#endregion
    }
}
