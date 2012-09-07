{#template MAIN}
{* this is comment *}

{#for index=1 to $T.list.length}
	{#if $T.index==1}
		<li data-role="list-divider" role="heading">{$T.data}</li>
	{#/if}
	{#param name=pay value=$T.list[$T.index-1]}
	<li><a href="{$P.pay.payurl}" _type="{$P.pay.paytype}" _url="{$P.pay.payid}" >
        <img src="{$P.pay.icon}"  onerror='this.src="/images/errorImg_small.jpg"' alt="{$P.pay.remark}" />
        <h3>
            {$P.pay.payname}</h3>
    </a></li>
{#/if}

{#/template MAIN}

