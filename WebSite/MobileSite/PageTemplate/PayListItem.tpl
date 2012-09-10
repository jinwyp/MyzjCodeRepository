{#template MAIN}
{* this is comment *}

{#for index=1 to $T.list.length}
	{#if $T.index==1}
		<li data-role="list-divider" role="heading">{$T.data}</li>
	{#/if}
	{#param name=pay value=$T.list[$T.index-1]}
	<li><a class="orderPayment" href="javascript:;" _type="{$P.pay.paytype}" _payid="{$P.pay.payid}" >
        <img src="{$P.pay.icon}"  onerror='this.src="/images/errorImg_small.jpg"' />
        <h3>
            {$P.pay.payname}</h3>
    </a></li>
{#/if}

{#/template MAIN}

