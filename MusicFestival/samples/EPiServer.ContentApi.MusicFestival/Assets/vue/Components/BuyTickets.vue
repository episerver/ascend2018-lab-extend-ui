<template>
    <div class="buyTickets-container">
        <button type="button" v-on:click="toggleDialog">Buy Tickets Now</button>
        <div v-if="showDialog" class="dialog">
            <p>Enter your email adress to get a special invite!</p>
            <input type="email" />
        </div>
    </div>
</template>

<script>
    export default {
        name: 'BuyTickets',
        props: [],
        data: function () {
            return {
                showDialog: false
            }
        },
        methods: {
            toggleDialog: function () {
                this.showDialog = !this.showDialog
                
                if (this.showDialog) {
                    // TODO: Disable the OPE overlays in the Hero
                }
            }
        }
    }
</script>

<style lang="less" scoped>
    @import '../../Styles/Common/variables.less';
    
    .buyTickets-container {
        /*position: relative;*/

        .dialog {

            position: absolute;
            left: 25%;
            right: 25%;
            top: 100%;
            min-height: 20rem;

            background: @backgroundColor;
            padding: @space;
            border-radius: 1em;

            
            
            
            /*position: fixed;*/
            /*background-color: rgba(255, 255, 255, 0.15);*/
            /*top: 0;*/
            /*right: 0;*/
            /*bottom: 0;*/
            /*left: 0;*/
            z-index: 999;
            /*pointer-events: none;*/
            -webkit-transition: all 0.3s;
            -moz-transition: all 0.3s;
            transition: all 0.3s;
        }
    }
    
</style>