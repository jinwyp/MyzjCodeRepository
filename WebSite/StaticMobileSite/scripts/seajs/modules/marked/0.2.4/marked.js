define("#marked/0.2.4/marked",[],function(a,b,c){(function(){function m(){var a="(?!(?:a|em|strong|small|s|cite|q|dfn|abbr|data|time|code|var|samp|kbd|sub|sup|i|b|u|mark|ruby|rt|rp|bdi|bdo|span|br|wbr|ins|del|img)\\b)\\w+";return a}function n(a){return a=a.source,function b(c,d){return c?(a=a.replace(c,d.source||d),b):new RegExp(a)}}function o(){}var a={newline:/^\n+/,code:/^( {4}[^\n]+\n*)+/,fences:o,hr:/^( *[\-*_]){3,} *(?:\n+|$)/,heading:/^ *(#{1,6}) *([^\n]+?) *#* *(?:\n+|$)/,lheading:/^([^\n]+)\n *(=|-){3,} *\n*/,blockquote:/^( *>[^\n]+(\n[^\n]+)*\n*)+/,list:/^( *)([*+-]|\d+\.) [^\0]+?(?:\n{2,}(?! )(?!\1bullet)\n*|\s*$)/,html:/^ *(?:comment|closed|closing) *(?:\n{2,}|\s*$)/,def:/^ *\[([^\]]+)\]: *([^\s]+)(?: +["(]([^\n]+)[")])? *(?:\n+|$)/,paragraph:/^([^\n]+\n?(?!body))+\n*/,text:/^[^\n]+/};a.list=n(a.list)("bullet",/(?:[*+-](?!(?: *[-*]){2,})|\d+\.)/)(),a.html=n(a.html)("comment",/<!--[^\0]*?-->/)("closed",/<(tag)[^\0]+?<\/\1>/)("closing",/<tag(?!:\/|@)\b(?:"[^"]*"|'[^']*'|[^'">])*?>/)(/tag/g,m())(),a.paragraph=function(){var b=a.paragraph.source,c=[];return function d(b){return b=a[b]?a[b].source:b,c.push(b.replace(/(^|[^\[])\^/g,"$1")),d}("hr")("heading")("lheading")("blockquote")("<"+m())("def"),new RegExp(b.replace("body",c.join("|")))}(),a.normal={fences:a.fences,paragraph:a.paragraph},a.gfm={fences:/^ *``` *(\w+)? *\n([^\0]+?)\s*``` *(?:\n+|$)/,paragraph:/^/},a.gfm.paragraph=n(a.paragraph)("(?!","(?!"+a.gfm.fences.source.replace(/(^|[^\[])\^/g,"$1")+"|")(),a.lexer=function(b){var c=[];return c.links={},b=b.replace(/\r\n|\r/g,"\n").replace(/\t/g,"    "),a.token(b,c,!0)},a.token=function(b,c,d){var b=b.replace(/^ +$/gm,""),e,f,g,h,i,j,k;while(b){if(g=a.newline.exec(b))b=b.substring(g[0].length),g[0].length>1&&c.push({type:"space"});if(g=a.code.exec(b)){b=b.substring(g[0].length),g=g[0].replace(/^ {4}/gm,""),c.push({type:"code",text:q.pedantic?g:g.replace(/\n+$/,"")});continue}if(g=a.fences.exec(b)){b=b.substring(g[0].length),c.push({type:"code",lang:g[1],text:g[2]});continue}if(g=a.heading.exec(b)){b=b.substring(g[0].length),c.push({type:"heading",depth:g[1].length,text:g[2]});continue}if(g=a.lheading.exec(b)){b=b.substring(g[0].length),c.push({type:"heading",depth:g[2]==="="?1:2,text:g[1]});continue}if(g=a.hr.exec(b)){b=b.substring(g[0].length),c.push({type:"hr"});continue}if(g=a.blockquote.exec(b)){b=b.substring(g[0].length),c.push({type:"blockquote_start"}),g=g[0].replace(/^ *> ?/gm,""),a.token(g,c,d),c.push({type:"blockquote_end"});continue}if(g=a.list.exec(b)){b=b.substring(g[0].length),c.push({type:"list_start",ordered:isFinite(g[2])}),g=g[0].match(/^( *)([*+-]|\d+\.) [^\n]*(?:\n(?!\1(?:[*+-]|\d+\.) )[^\n]*)*/gm),e=!1,k=g.length,j=0;for(;j<k;j++)h=g[j],i=h.length,h=h.replace(/^ *([*+-]|\d+\.) +/,""),~h.indexOf("\n ")&&(i-=h.length,h=q.pedantic?h.replace(/^ {1,4}/gm,""):h.replace(new RegExp("^ {1,"+i+"}","gm"),"")),f=e||/\n\n(?!\s*$)/.test(h),j!==k-1&&(e=h[h.length-1]==="\n",f||(f=e)),c.push({type:f?"loose_item_start":"list_item_start"}),a.token(h,c),c.push({type:"list_item_end"});c.push({type:"list_end"});continue}if(g=a.html.exec(b)){b=b.substring(g[0].length),c.push({type:"html",pre:g[1]==="pre",text:g[0]});continue}if(d&&(g=a.def.exec(b))){b=b.substring(g[0].length),c.links[g[1].toLowerCase()]={href:g[2],title:g[3]};continue}if(d&&(g=a.paragraph.exec(b))){b=b.substring(g[0].length),c.push({type:"paragraph",text:g[0]});continue}if(g=a.text.exec(b)){b=b.substring(g[0].length),c.push({type:"text",text:g[0]});continue}}return c};var b={escape:/^\\([\\`*{}\[\]()#+\-.!_>])/,autolink:/^<([^ >]+(@|:\/)[^ >]+)>/,url:o,tag:/^<!--[^\0]*?-->|^<\/?\w+(?:"[^"]*"|'[^']*'|[^'">])*?>/,link:/^!?\[(inside)\]\(href\)/,reflink:/^!?\[(inside)\]\s*\[([^\]]*)\]/,nolink:/^!?\[((?:\[[^\]]*\]|[^\[\]])*)\]/,strong:/^__([^\0]+?)__(?!_)|^\*\*([^\0]+?)\*\*(?!\*)/,em:/^\b_((?:__|[^\0])+?)_\b|^\*((?:\*\*|[^\0])+?)\*(?!\*)/,code:/^(`+)([^\0]*?[^`])\1(?!`)/,br:/^ {2,}\n(?!\s*$)/,text:/^[^\0]+?(?=[\\<!\[_*`]| {2,}\n|$)/};b._linkInside=/(?:\[[^\]]*\]|[^\]]|\](?=[^\[]*\]))*/,b._linkHref=/\s*<?([^\s]*?)>?(?:\s+['"]([^\0]*?)['"])?\s*/,b.link=n(b.link)("inside",b._linkInside)("href",b._linkHref)(),b.reflink=n(b.reflink)("inside",b._linkInside)(),b.normal={url:b.url,strong:b.strong,em:b.em,text:b.text},b.pedantic={strong:/^__(?=\S)([^\0]*?\S)__(?!_)|^\*\*(?=\S)([^\0]*?\S)\*\*(?!\*)/,em:/^_(?=\S)([^\0]*?\S)_(?!_)|^\*(?=\S)([^\0]*?\S)\*(?!\*)/},b.gfm={url:/^(https?:\/\/[^\s]+[^.,:;"')\]\s])/,text:/^[^\0]+?(?=[\\<!\[_*`]|https?:\/\/| {2,}\n|$)/},b.lexer=function(a){var c="",f=e.links,g,h,i,j;while(a){if(j=b.escape.exec(a)){a=a.substring(j[0].length),c+=j[1];continue}if(j=b.autolink.exec(a)){a=a.substring(j[0].length),j[2]==="@"?(h=j[1][6]===":"?l(j[1].substring(7)):l(j[1]),i=l("mailto:")+h):(h=k(j[1]),i=h),c+='<a href="'+i+'">'+h+"</a>";continue}if(j=b.url.exec(a)){a=a.substring(j[0].length),h=k(j[1]),i=h,c+='<a href="'+i+'">'+h+"</a>";continue}if(j=b.tag.exec(a)){a=a.substring(j[0].length),c+=q.sanitize?k(j[0]):j[0];continue}if(j=b.link.exec(a)){a=a.substring(j[0].length),c+=d(j,{href:j[2],title:j[3]});continue}if((j=b.reflink.exec(a))||(j=b.nolink.exec(a))){a=a.substring(j[0].length),g=(j[2]||j[1]).replace(/\s+/g," "),g=f[g.toLowerCase()];if(!g||!g.href){c+=j[0][0],a=j[0].substring(1)+a;continue}c+=d(j,g);continue}if(j=b.strong.exec(a)){a=a.substring(j[0].length),c+="<strong>"+b.lexer(j[2]||j[1])+"</strong>";continue}if(j=b.em.exec(a)){a=a.substring(j[0].length),c+="<em>"+b.lexer(j[2]||j[1])+"</em>";continue}if(j=b.code.exec(a)){a=a.substring(j[0].length),c+="<code>"+k(j[2],!0)+"</code>";continue}if(j=b.br.exec(a)){a=a.substring(j[0].length),c+="<br>";continue}if(j=b.text.exec(a)){a=a.substring(j[0].length),c+=k(j[0]);continue}}return c};var d=function(a,c){return a[0][0]!=="!"?'<a href="'+k(c.href)+'"'+(c.title?' title="'+k(c.title)+'"':"")+">"+b.lexer(a[1])+"</a>":'<img src="'+k(c.href)+'" alt="'+k(a[1])+'"'+(c.title?' title="'+k(c.title)+'"':"")+">"},e,f,g=function(){return f=e.pop()},h=function(){switch(f.type){case"space":return"";case"hr":return"<hr>\n";case"heading":return"<h"+f.depth+">"+b.lexer(f.text)+"</h"+f.depth+">\n";case"code":return q.highlight&&(f.code=q.highlight(f.text,f.lang),f.code!=null&&f.code!==f.text&&(f.escaped=!0,f.text=f.code)),f.escaped||(f.text=k(f.text,!0)),"<pre><code"+(f.lang?' class="lang-'+f.lang+'"':"")+">"+f.text+"</code></pre>\n";case"blockquote_start":var a="";while(g().type!=="blockquote_end")a+=h();return"<blockquote>\n"+a+"</blockquote>\n";case"list_start":var c=f.ordered?"ol":"ul",a="";while(g().type!=="list_end")a+=h();return"<"+c+">\n"+a+"</"+c+">\n";case"list_item_start":var a="";while(g().type!=="list_item_end")a+=f.type==="text"?i():h();return"<li>"+a+"</li>\n";case"loose_item_start":var a="";while(g().type!=="list_item_end")a+=h();return"<li>"+a+"</li>\n";case"html":return q.sanitize?b.lexer(f.text):!f.pre&&!q.pedantic?b.lexer(f.text):f.text;case"paragraph":return"<p>"+b.lexer(f.text)+"</p>\n";case"text":return"<p>"+i()+"</p>\n"}},i=function(){var a=f.text,c;while((c=e[e.length-1])&&c.type==="text")a+="\n"+g().text;return b.lexer(a)},j=function(a){e=a.reverse();var b="";while(g())b+=h();return e=null,f=null,b},k=function(a,b){return a.replace(b?/&/g:/&(?!#?\w+;)/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/"/g,"&quot;").replace(/'/g,"&#39;")},l=function(a){var b="",c=a.length,d=0,e;for(;d<c;d++)e=a.charCodeAt(d),Math.random()>.5&&(e="x"+e.toString(16)),b+="&#"+e+";";return b};o.exec=o;var p=function(b,c){return s(c),j(a.lexer(b))},q,r,s=function(c){c||(c=r);if(q===c)return;q=c,q.gfm?(a.fences=a.gfm.fences,a.paragraph=a.gfm.paragraph,b.text=b.gfm.text,b.url=b.gfm.url):(a.fences=a.normal.fences,a.paragraph=a.normal.paragraph,b.text=b.normal.text,b.url=b.normal.url),q.pedantic?(b.em=b.pedantic.em,b.strong=b.pedantic.strong):(b.em=b.normal.em,b.strong=b.normal.strong)};p.options=p.setOptions=function(a){return r=a,s(a),p},p.options({gfm:!0,pedantic:!1,sanitize:!1,highlight:null}),p.parser=function(a,b){return s(b),j(a)},p.lexer=function(b,c){return s(c),a.lexer(b)},p.parse=p,typeof c!="undefined"?c.exports=p:this.marked=p}).call(function(){return this||(typeof window!="undefined"?window:global)}())});